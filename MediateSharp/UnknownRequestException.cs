namespace MediateSharp;

public class UnknownRequestException : Exception
{
    internal UnknownRequestException() : base("Unknown Request.")
    {
    }
}