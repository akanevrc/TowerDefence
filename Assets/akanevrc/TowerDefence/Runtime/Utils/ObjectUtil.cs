using System;

namespace akanevrc.TowerDefence
{
    public static class ObjectUtil
    {
        public static T Assign<T>(this T obj, out T assigned)
        {
            assigned = obj;
            return obj;
        }

        public static int KindToInt<T>(this T kind)
            where T : struct
        {
            if (kind is Enum)
            {
                return (int)(object)kind;
            }
            else if (kind is IKindType<T> k)
            {
                return k.ToInt();
            }
            else
            {
                throw new ArgumentException(nameof(kind));
            }
        }

        public static T IntToKind<T>(this int i)
            where T : struct
        {
            if (typeof(T).IsEnum)
            {
                return (T)(object)i;
            }
            else if (typeof(IKindType<T>).IsAssignableFrom(typeof(T)))
            {
                return ((IKindType<T>)default(T)).FromInt(i);
            }
            else
            {
                throw new ArgumentException(nameof(i));
            }
        }
    }
}
