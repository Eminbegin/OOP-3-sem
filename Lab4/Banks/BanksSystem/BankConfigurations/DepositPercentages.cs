using Banks.Exceptions;

namespace Banks.BanksSystem.BankConfigurations;

public class DepositPercentages : IEquatable<DepositPercentages>
{
    private readonly List<DepositPair> _pairs;
    private DepositPercentages(List<DepositPair> pairs)
    {
        _pairs = pairs;
    }

    public static IPairsBuilder Builder => new PairsBuilder();
    public static DepositPercentages Empty => new DepositPercentages(new List<DepositPair>());

    public decimal GetPercentage(decimal value)
    {
        decimal result = _pairs[0].Percentage;
        foreach (DepositPair pair in _pairs.TakeWhile(pair => pair.Value <= value))
        {
            result = pair.Percentage;
        }

        return result;
    }

    public bool Equals(DepositPercentages? other)
    {
        return other is not null && _pairs.Equals(other._pairs);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as DepositPercentages);
    }

    public override int GetHashCode()
    {
        return _pairs.GetHashCode();
    }

    private class DepositPair
    {
        public DepositPair(decimal value, decimal percentage)
        {
            Value = value;
            Percentage = percentage;
        }

        public decimal Value { get; }
        public decimal Percentage { get; }

        public bool AreValuesEquals(DepositPair depositPair)
        {
            return Value == depositPair.Value;
        }

        public decimal GreaterPercentage(DepositPair depositPair)
        {
            return Percentage > depositPair.Percentage ? Percentage : depositPair.Percentage;
        }
    }

    private class PairsBuilder : IPairsBuilder
    {
        private readonly List<DepositPair> _pairs;

        public PairsBuilder()
        {
            _pairs = new List<DepositPair>();
        }

        public IPairsBuilder AddPair(decimal value, decimal percentage)
        {
            if (value <= 0 || percentage < 0)
            {
                throw ExistenceException.BadValue();
            }

            decimal newPercentage = -1;
            var newPair = new DepositPair(value, percentage);
            foreach (DepositPair pair in _pairs)
            {
                if (pair.AreValuesEquals(newPair))
                {
                    newPercentage = pair.GreaterPercentage(newPair);
                }
            }

            if (newPercentage > percentage)
            {
                _pairs.Remove(_pairs.First(p => p.AreValuesEquals(newPair)));
                _pairs.Add(new DepositPair(value, newPercentage));
                return this;
            }

            if (newPercentage != -1)
            {
                return this;
            }

            _pairs.Add(newPair);
            return this;
        }

        public DepositPercentages Build(decimal percentage)
        {
            if (percentage < 0)
            {
                throw ExistenceException.BadValue();
            }

            _pairs.Add(new DepositPair(0, percentage));
            return new DepositPercentages(_pairs
                .OrderBy(p => p.Percentage)
                .Select(p => new DepositPair(p.Value, p.Percentage / 100))
                .ToList());
        }
    }
}

public interface IPairsBuilder
{
    IPairsBuilder AddPair(decimal value, decimal percentage);
    DepositPercentages Build(decimal percentage);
}