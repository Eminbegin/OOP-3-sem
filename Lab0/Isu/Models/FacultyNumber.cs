using Isu.Exceptions;

namespace Isu.Models;

public class FacultyNumber
{
    private const int FacultyLetterIndex = 0;

    public FacultyNumber(string name)
    {
        Value = GetFacultyNumber(name);
    }

    public char Value { get; }

    private char GetFacultyNumber(string name)
    {
        char facultyNumber = name[FacultyLetterIndex];
        if (!char.IsUpper(facultyNumber))
        {
            throw new InvalidFacultyException(name[FacultyLetterIndex]);
        }

        return facultyNumber;
    }
}