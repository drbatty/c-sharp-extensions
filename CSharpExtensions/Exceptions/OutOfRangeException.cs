using System;

namespace CSharpExtensions.Exceptions
{
    public class OutOfRangeException : Exception
    {
        private readonly int _leftBound;
        private readonly int _rightBound;
        private readonly int _outOfRangeInteger;

        public OutOfRangeException(int lB, int rB, int oOri)
        {
            _leftBound = lB;
            _rightBound = rB;
            _outOfRangeInteger = oOri;
        }

        public override string ToString()
        {
            return _outOfRangeInteger + " IS NOT IN THE RANGE [" + _leftBound + "," + _rightBound + "].";
        }
    }
}