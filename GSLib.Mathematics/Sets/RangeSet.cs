//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using GSLib.Collections;
//using GSLib.Mathematics.Tuples;

//namespace GSLib.Mathematics.Sets
//{
//    public class RangeSet<T>
//    {
//        OrderedPair<T> limits;
//        OrderedPair<bool> inclusive;
//        Comparer<T> comparer;

//        public T LowerLimit 
//        {
//            get
//            {
//                return limits[0];
//            }
//            set
//            {
//                if (Helpers.IsComparable<T>())
//                {
//                    if (Helpers.Compare(value, UpperLimit))
//                }
//                    limits[0] = value;
//                else
//                    throw new ArgumentOutOfRangeException("LowerLimit must be less than UpperLimit.");
//            }
//        }

//        public T UpperLimit
//        {
//            get
//            {
//                return limits[1];
//            }
//            set
//            {
//                if (value.CompareTo(LowerLimit) > 0)
//                    limits[0] = value;
//                else throw new ArgumentOutOfRangeException("UpperLimit must be greater than LowerLimit.");
//            }
//        }

//        public RangeSet(T lowerLimit, T upperLimit, bool lowerInclusive, bool upperInclusive)
//        {
//            if (upperLimit.CompareTo(lowerLimit) > 0)
//            {
//                limits = new OrderedPair<T>(lowerLimit, upperLimit);
//                inclusive = new OrderedPair<bool>(lowerInclusive, upperInclusive);
//            }
//            else
//            {
//                throw new ArgumentOutOfRangeException("UpperLimit must be greater than LowerLimit.");
//            }
//        }

//        public bool IsInRange(T value)
//        {
//            int lcmpr = value.CompareTo(value);

//        }
//    }
//}
