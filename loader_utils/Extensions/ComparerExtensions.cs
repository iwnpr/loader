using System.Diagnostics.CodeAnalysis;

namespace loader_utils
{
    public static class ComparerExtensions
    {
        #region IsNullChecks
        private static bool IsNull<T1, T2>([NotNullWhen(false)] Nullable<T1> a, [NotNullWhen(false)] Nullable<T2> b) where T1 : struct where T2 : struct
        {
            return a == null || b == null;
        }

        private static bool IsNull<T>([NotNullWhen(false)] string? a, [NotNullWhen(false)] Nullable<T> b) where T : struct
        {
            return !a.IsNotNull() || b == null;
        }

        private static bool IsNull([NotNullWhen(false)] string? a, [NotNullWhen(false)] string? b)
        {
            return !a.IsNotNull() || !b.IsNotNull();
        }

        private static bool IsNull([NotNullWhen(false)] DateTime? a, [NotNullWhen(false)] string? b)
        {
            return a == null || a == DateTime.MinValue || !b.IsNotNull();
        }

        private static bool IsNull([NotNullWhen(false)] DateTime? a, [NotNullWhen(false)] DateTime? b)
        {
            return a == null || a == DateTime.MinValue || b == null || b == DateTime.MinValue;
        }

        private static bool IsNull([NotNullWhen(false)] string? a, [NotNullWhen(false)] DateTime? b)
        {
            return !a.IsNotNull() || b == null || b == DateTime.MinValue;
        }
        #endregion

        #region DateTime

        #region DateTime/String
        public static bool DateLess(this DateTime a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return a < DateTime.Parse(b);
        }

        public static bool DateLessEqual(this DateTime a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return a <= DateTime.Parse(b);
        }

        public static bool DateGreater(this DateTime a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return a > DateTime.Parse(b);
        }

        public static bool DateGreaterEqual(this DateTime a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return a >= DateTime.Parse(b);
        }

        public static bool DateEqual(this DateTime a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return a == DateTime.Parse(b);
        }

        public static bool DateNotEqual(this DateTime a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return a != DateTime.Parse(b);
        }
        #endregion

        #region DateTime/DateTime
        public static bool DateLess(this DateTime a, DateTime? b)
        {
            if (IsNull(a, b))
                return false;
            return a < b;
        }

        public static bool DateLessEqual(this DateTime a, DateTime? b)
        {
            if (IsNull(a, b))
                return false;
            return a <= b;
        }

        public static bool DateGreater(this DateTime a, DateTime? b)
        {
            if (IsNull(a, b))
                return false;
            return a > b;
        }

        public static bool DateGreaterEqual(this DateTime a, DateTime? b)
        {
            if (IsNull(a, b))
                return false;
            return a >= b;
        }

        public static bool DateNotEqual(this DateTime a, DateTime? b)
        {
            if (IsNull(a, b))
                return false;
            return a != b;
        }
        #endregion

        #region DateTime?/String
        public static bool DateLess(this DateTime? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return a < DateTime.Parse(b);
        }

        public static bool DateLessEqual(this DateTime? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return a <= DateTime.Parse(b);
        }

        public static bool DateGreater(this DateTime? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return a > DateTime.Parse(b);
        }

        public static bool DateGreaterEqual(this DateTime? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return a >= DateTime.Parse(b);
        }
        #endregion

        #region DateTime?/DateTime
        public static bool DateLess(this DateTime? a, DateTime? b)
        {
            if (IsNull(a, b))
                return false;
            return a < b;
        }

        public static bool DateLessEqual(this DateTime? a, DateTime? b)
        {
            if (IsNull(a, b))
                return false;
            return a <= b;
        }

        public static bool DateGreater(this DateTime? a, DateTime? b)
        {
            if (IsNull(a, b))
                return false;
            return a > b;
        }

        public static bool DateGreaterEqual(this DateTime? a, DateTime? b)
        {
            if (IsNull(a, b))
                return false;
            return a >= b;
        }
        #endregion

        #region String/String
        public static bool DateLess(this string? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return DateTime.Parse(a) < DateTime.Parse(b);
        }

        public static bool DateLess(this string? a, string? b, int years)
        {
            if (IsNull(a, b))
                return false;
            return DateTime.Parse(a) < DateTime.Parse(b).AddYears(years);
        }

        public static bool DateLessEqual(this string? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return DateTime.Parse(a) <= DateTime.Parse(b);
        }

        public static bool DateGreater(this string? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return DateTime.Parse(a) > DateTime.Parse(b);
        }

        public static bool DateGreater(this string? a, string? b, int years)
        {
            if (IsNull(a, b))
                return false;
            return DateTime.Parse(a) > DateTime.Parse(b).AddYears(years);
        }

        public static bool DateGreaterEqual(this string? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return DateTime.Parse(a) >= DateTime.Parse(b);
        }

        public static bool DateNotEqual(this string? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return DateTime.Parse(a) != DateTime.Parse(b);
        }
        #endregion

        #region String/DateTime
        public static bool DateLess(this string? a, DateTime? b)
        {
            if (IsNull(a, b))
                return false;
            return DateTime.Parse(a) < b;
        }

        public static bool DateLess(this string? a, DateTime? b, int years)
        {
            if (IsNull(a, b))
                return false;
            return DateTime.Parse(a) < ((DateTime)b).AddYears(years);
        }

        public static bool DateLessEqual(this string? a, DateTime? b)
        {
            if (IsNull(a, b))
                return false;
            return DateTime.Parse(a) <= b;
        }

        public static bool DateGreater(this string? a, DateTime? b)
        {
            if (IsNull(a, b))
                return false;
            return DateTime.Parse(a) > b;
        }

        public static bool DateGreaterEqual(this string? a, DateTime? b)
        {
            if (IsNull(a, b))
                return false;
            return DateTime.Parse(a) >= b;
        }

        public static bool DateNotEqual(this string? a, DateTime? b)
        {
            if (IsNull(a, b))
                return false;
            return DateTime.Parse(a) != b;
        }
        #endregion

        #endregion

        #region Decimal

        #region Decimal?/String
        public static bool DecimalLess(this decimal? a, string? b)
        {
            if (!b.IsNotNull())
                return false;
            return a < decimal.Parse(b);
        }

        public static bool DecimalLessEqual(this decimal? a, string? b)
        {
            if (!b.IsNotNull())
                return false;
            return a <= decimal.Parse(b);
        }

        public static bool DecimalGreater(this decimal? a, string? b)
        {
            if (!b.IsNotNull())
                return false;
            return a > decimal.Parse(b);
        }

        public static bool DecimalGreaterEqual(this decimal? a, string? b)
        {
            if (!b.IsNotNull())
                return false;
            return a >= decimal.Parse(b);
        }
        #endregion

        #region Decimal?/Decimal
        public static bool DecimalLess(this decimal? a, decimal? b)
        {
            if (IsNull(a, b))
                return false;
            return a < b;
        }

        public static bool DecimalLessEqual(this decimal? a, decimal? b)
        {
            if (IsNull(a, b))
                return false;
            return a <= b;
        }

        public static bool DecimalGreater(this decimal? a, decimal? b)
        {
            if (IsNull(a, b))
                return false;
            return a > b;
        }

        public static bool DecimalGreaterEqual(this decimal? a, decimal? b)
        {
            if (IsNull(a, b))
                return false;
            return a >= b;
        }
        #endregion

        #region String/String
        public static bool DecimalLess(this string? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return decimal.Parse(a) < decimal.Parse(b);
        }

        public static bool DecimalLessEqual(this string? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return decimal.Parse(a) <= decimal.Parse(b);
        }

        public static bool DecimalGreater(this string? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return decimal.Parse(a) > decimal.Parse(b);
        }

        public static bool DecimalGreaterEqual(this string? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return decimal.Parse(a) >= decimal.Parse(b);
        }

        public static bool DecimalEqual(this string? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return decimal.Parse(a) == decimal.Parse(b);
        }

        public static bool DecimalNotEqual(this string? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return decimal.Parse(a) != decimal.Parse(b);
        }
        #endregion

        #region String/Decimal

        public static bool DecimalLess(this string? a, decimal? b)
        {
            if (IsNull(a, b))
                return false;
            return decimal.Parse(a) < b;
        }

        public static bool DecimalLessEqual(this string? a, decimal? b)
        {
            if (IsNull(a, b))
                return false;
            return decimal.Parse(a) <= b;
        }

        public static bool DecimalGreater(this string? a, decimal? b)
        {
            if (IsNull(a, b))
                return false;
            return decimal.Parse(a) > b;
        }

        public static bool DecimalGreaterEqual(this string? a, decimal? b)
        {
            if (IsNull(a, b))
                return false;
            return decimal.Parse(a) >= b;
        }

        public static bool DecimalEqual(this string? a, decimal? b)
        {
            if (IsNull(a, b))
                return false;
            return decimal.Parse(a) == b;
        }

        public static bool DecimalNotEqual(this string? a, decimal? b)
        {
            if (IsNull(a, b))
                return false;
            return decimal.Parse(a) != b;
        }
        #endregion

        #endregion

        #region Int

        #region String/String

        public static bool IntLess(this string? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return decimal.Parse(a) < decimal.Parse(b);
        }

        public static bool IntLessEqual(this string? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return decimal.Parse(a) <= decimal.Parse(b);
        }

        public static bool IntGreater(this string? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return decimal.Parse(a) > decimal.Parse(b);
        }

        public static bool IntGreaterEqual(this string? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return decimal.Parse(a) >= decimal.Parse(b);
        }

        public static bool IntEqual(this string? a, string? b)
        {
            if (IsNull(a, b))
                return false;
            return decimal.Parse(a) == decimal.Parse(b);
        }
        #endregion

        #region String/Int

        public static bool IntLess(this string? a, int? b)
        {
            if (IsNull(a, b))
                return false;
            return int.Parse(a) < b;
        }

        public static bool IntLessEqual(this string? a, int? b)
        {
            if (IsNull(a, b))
                return false;
            return int.Parse(a) <= b;
        }

        public static bool IntGreater(this string? a, int? b)
        {
            if (IsNull(a, b))
                return false;
            return int.Parse(a) > b;
        }

        public static bool IntGreaterEqual(this string? a, int? b)
        {
            if (IsNull(a, b))
                return false;
            return int.Parse(a) >= b;
        }

        public static bool IntEqual(this string? a, int? b)
        {
            if (IsNull(a, b))
                return false;
            return int.Parse(a) == b;
        }
        #endregion

        #endregion
    }
}
