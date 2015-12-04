namespace CSharpExtensions
{
    /// <summary>Return type to return the result of a method and a boolean indicating whether or not the method was a success</summary>
    public class MethodResult<T>
    {
        public T ReturnValue { get; set; }
        public bool Success { get; set; }
        public string ErrorString { get; set; }
    }
}