namespace HomeLib.Core.Exceptions;

public class BadRequestException(string message) : Exception(message);