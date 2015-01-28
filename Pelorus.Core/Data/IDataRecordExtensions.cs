using System;
using System.Data;
using System.Xml;

namespace Pelorus.Core.Data
{
    public static class IDataRecordExtensions
    {
        /// <summary>
        /// Gets the value of the specified column as a boolean.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Boolean value of the specified column.</returns>
        public static bool GetBoolean(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            return reader.GetBoolean(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable boolean.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Boolean or null value of the specified column.</returns>
        public static Nullable<bool> GetNullableBoolean(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            var value = reader.GetValue(ordinal);

            if (DBNull.Value == value)
            {
                return null;
            }

            return (bool)value;
        }

        /// <summary>
        /// Gets the value of the specified column as a byte.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Byte value of the specified column.</returns>
        public static byte GetByte(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            return reader.GetByte(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable byte.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Byte or null value of the specified column.</returns>
        public static Nullable<byte> GetNullableByte(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            var value = reader.GetValue(ordinal);

            if (DBNull.Value == value)
            {
                return null;
            }

            return (byte)value;
        }

        /// <summary>
        /// Reads a stream of bytes from the specified column offset into the buffer as an array, starting at the given buffer 
        /// offset.
        /// </summary>
        /// <param name="reader">Reader to get the bytes from.</param>
        /// <param name="name">Name of the column to get the bytes from.</param>
        /// <param name="fieldOffset">The index within the field from which to start the read operation.</param>
        /// <param name="buffer">The buffer into which to read the stream of bytes.</param>
        /// <param name="bufferoffset">The index for buffer to start the read operation.</param>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>The actual number of bytes read.</returns>
        public static long GetBytes(this IDataRecord reader, string name, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            return reader.GetBytes(ordinal, fieldOffset, buffer, bufferoffset, length);
        }

        /// <summary>
        /// Gets the value of the specified column as a char.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Char value of the specified column.</returns>
        public static char GetChar(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            return reader.GetChar(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable char.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Char or null value of the specified column.</returns>
        public static Nullable<char> GetNullableChar(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            var value = reader.GetValue(ordinal);

            if (DBNull.Value == value)
            {
                return null;
            }

            return (char)value;
        }

        /// <summary>
        /// Reads a stream of characters from the specified column offset into the buffer as an array, starting at the given 
        /// buffer offset.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <param name="fieldoffset">The index within the row from which to start the read operation.</param>
        /// <param name="buffer">The buffer into which to read the stream of bytes.</param>
        /// <param name="bufferoffset">The index for buffer to start the read operation.</param>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>The actual number of characters read.</returns>
        public static long GetChars(this IDataRecord reader, string name, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            return reader.GetChars(ordinal, fieldoffset, buffer, bufferoffset, length);
        }

        /// <summary>
        /// Gets the value of the specified column as a DateTime.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>DateTime value of the specified column.</returns>
        public static DateTime GetDateTime(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            return reader.GetDateTime(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable DateTime.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>DateTime or null value of the specified column.</returns>
        public static Nullable<DateTime> GetNullableDateTime(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            var value = reader.GetValue(ordinal);

            if (DBNull.Value == value)
            {
                return null;
            }

            return (DateTime)value;
        }

        /// <summary>
        /// Gets the value of the specified column as a decimal.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Decimal value of the specified column.</returns>
        public static decimal GetDecimal(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            return reader.GetDecimal(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable decimal.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Decimal or null value of the specified column.</returns>
        public static Nullable<decimal> GetNullableDecimal(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            var value = reader.GetValue(ordinal);

            if (DBNull.Value == value)
            {
                return null;
            }

            return (decimal)value;
        }

        /// <summary>
        /// Gets the value of the specified column as a double.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Double value of the specified column.</returns>
        public static double GetDouble(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            return reader.GetDouble(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable double.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Double or null value of the specified column.</returns>
        public static Nullable<double> GetNullableDouble(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            var value = reader.GetValue(ordinal);

            if (DBNull.Value == value)
            {
                return null;
            }

            return (double)value;
        }

        /// <summary>
        /// Gets the value of the specified column as a float.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Float value of the specified column.</returns>
        public static float GetFloat(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            return reader.GetFloat(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable float.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Float or null value of the specified column.</returns>
        public static Nullable<float> GetNullableFloat(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            var value = reader.GetValue(ordinal);

            if (DBNull.Value == value)
            {
                return null;
            }

            return (float)value;
        }

        /// <summary>
        /// Gets the value of the specified column as a Guid.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Guid value of the specified column.</returns>
        public static Guid GetGuid(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            return reader.GetGuid(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable Guid.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Guid or null value of the specified column.</returns>
        public static Nullable<Guid> GetNullableGuid(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            var value = reader.GetValue(ordinal);

            if (DBNull.Value == value)
            {
                return null;
            }

            return (Guid)value;
        }

        /// <summary>
        /// Gets the value of the specified column as an Int16.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Int16 value of the specified column.</returns>
        public static short GetInt16(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            return reader.GetInt16(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable Int16.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Int16 or null value of the specified column.</returns>
        public static Nullable<short> GetNullableInt16(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            var value = reader.GetValue(ordinal);

            if (DBNull.Value == value)
            {
                return null;
            }

            return (short)value;
        }

        /// <summary>
        /// Gets the value of the specified column as an Int32.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Int32 value of the specified column.</returns>
        public static int GetInt32(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            return reader.GetInt32(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable Int32.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Int32 or null value of the specified column.</returns>
        public static Nullable<int> GetNullableInt32(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            var value = reader.GetValue(ordinal);

            if (DBNull.Value == value)
            {
                return null;
            }

            return (short)value;
        }

        /// <summary>
        /// Gets the value of the specified column as an Int64.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Int64 value of the specified column.</returns>
        public static long GetInt64(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            return reader.GetInt64(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable Int64.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Int64 or null value of the specified column.</returns>
        public static Nullable<long> GetNullableInt64(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            var value = reader.GetValue(ordinal);

            if (DBNull.Value == value)
            {
                return null;
            }

            return (short)value;
        }

        /// <summary>
        /// Gets the value of the specified column as a string.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>String value of the specified column.</returns>
        public static string GetString(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            return reader.GetString(ordinal);
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable string.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>String value of the specified column.</returns>
        public static string GetNullableString(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            var value = reader.GetValue(ordinal);

            if (DBNull.Value == value)
            {
                return null;
            }

            return (string)value;
        }

        /// <summary>
        /// Gets the value of the specified column as an XML document.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>XML value of the specified column.</returns>
        public static XmlDocument GetXml(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            string xmlString = reader.GetString(ordinal);
            var xml = new XmlDocument();
            xml.InnerXml = xmlString;

            return xml;
        }

        /// <summary>
        /// Gets the value of the specified column as a nullable XML document.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>XML value of the specified column.</returns>
        public static XmlDocument GetNullableXml(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            var value = reader.GetValue(ordinal);

            if (DBNull.Value == value)
            {
                return null;
            }

            var xml = new XmlDocument();
            xml.InnerXml = (string)value;

            return xml;
        }

        /// <summary>
        /// Gets the value of the specified column.
        /// </summary>
        /// <param name="reader">Reader to get the value from.</param>
        /// <param name="name">Name of the column to get the value of.</param>
        /// <returns>Value or null value of the specified column.</returns>

        public static object GetValue(this IDataRecord reader, string name)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            int ordinal = reader.GetOrdinal(name);
            return reader.GetValue(ordinal);
        }
    }
}
