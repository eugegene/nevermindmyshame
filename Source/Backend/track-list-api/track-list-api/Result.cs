namespace api;

/// <summary>
/// class for returning Result type. Contains IsSuccess, Error message and Data.
/// call Ok() if method ended successfully
/// call Fail() if method ended with an error
/// 
/// data field will be default, if operation wasn't successful
/// </summary>
public class Result
{
    protected Result(bool isSuccess, string error)
    {
        if (isSuccess && !string.IsNullOrEmpty(error))
            throw new InvalidOperationException("If operation not successful message must NOT be passed");
        
        if (IsFailure && string.IsNullOrEmpty(error))
            throw new InvalidOperationException("If operation not successful message must be passed");
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public string Error { get; }

    public static Result Ok() => new(true, string.Empty);

    public static Result<T> Ok<T>(T value) =>  new(value, true, string.Empty);

    public static Result Fail(string error) => new(false, error);
    
    //FIXME чи треба дефолт тут на щось міняти? Говорить що боїться нал значення, та це хороший консьорн
    public static Result<T> Fail<T>(string error) => new(default, false, error);
    
}

public class Result<T> : Result
{
    protected internal Result(T value, bool isSuccess, string error) : base(isSuccess, error)
    {
        Value = value;
    }

    public T Value { get; set; }
}