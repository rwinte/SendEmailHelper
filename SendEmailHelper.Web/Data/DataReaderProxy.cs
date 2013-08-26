using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

namespace SendEmailHelper.Web.Data
{
    public class SqlDataReaderProxy : SqlBase, IDataReader
    {
        private SqlDataReader _reader;

        public SqlDataReaderProxy(string command, CommandType commandType)
        {
            Connection = new SqlConnection(ConnectionString);
            Command = new SqlCommand(command, Connection) {CommandType = commandType};
        }

        public IDbDataParameter CreateParameter()
        {
            return Command.CreateParameter();
        }

        public string CommandText
        {
            get { return Command.CommandText; }
            set { Command.CommandText = value; }
        }

        public int CommandTimeout
        {
            get { return Command.CommandTimeout; }
            set { Command.CommandTimeout = value; }
        }

        public IDataParameterCollection Parameters
        {
            get { return Command.Parameters; }
        }

        public void Dispose()
        {
            _reader.Dispose();
            Command.Dispose();
            Connection.Dispose();
        }

        public string GetName(int i)
        {
            return _reader.GetName(i);
        }

        public string GetDataTypeName(int i)
        {
            return _reader.GetDataTypeName(i);
        }

        public Type GetFieldType(int i)
        {
            return _reader.GetFieldType(i);
        }

        public object GetValue(int i)
        {
            return _reader.GetFieldType(i);
        }

        public int GetValues(object[] values)
        {
            return _reader.GetValues(values);
        }

        public int GetOrdinal(string name)
        {
            return _reader.GetOrdinal(name);
        }

        public bool GetBoolean(int i)
        {
            return _reader.GetBoolean(i);
        }

        public byte GetByte(int i)
        {
            return _reader.GetByte(i);
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return _reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        }

        public char GetChar(int i)
        {
            return _reader.GetChar(i);
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            return _reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
        }

        public Guid GetGuid(int i)
        {
            return _reader.GetGuid(i);
        }

        public short GetInt16(int i)
        {
            return _reader.GetInt16(i);
        }

        public int GetInt32(int i)
        {
            return _reader.GetInt32(i);
        }

        public long GetInt64(int i)
        {
            return _reader.GetInt64(i);
        }

        public float GetFloat(int i)
        {
            return _reader.GetFloat(i);
        }

        public double GetDouble(int i)
        {
            return _reader.GetDouble(i);
        }

        public string GetString(int i)
        {
            return _reader.GetString(i);
        }

        public decimal GetDecimal(int i)
        {
            return _reader.GetDecimal(i);
        }

        public DateTime GetDateTime(int i)
        {
            return _reader.GetDateTime(i);
        }

        public IDataReader GetData(int i)
        {
            return _reader.GetData(i);
        }

        public bool IsDBNull(int i)
        {
            return _reader.IsDBNull(i);
        }

        public int FieldCount
        {
            get { return _reader.FieldCount; }
        }

        public object this[int i]
        {
            get { return _reader[i]; }
        }

        public object this[string name]
        {
            get { return _reader[name]; }
        }

        public void Close()
        {
            _reader.Close();
        }

        public DataTable GetSchemaTable()
        {
            return _reader.GetSchemaTable();
        }

        public bool NextResult()
        {
            return _reader.NextResult();
        }

        public bool Read()
        {
            if (_reader == null || _reader.IsClosed)
            {
                Connection.Open();
                _reader = Command.ExecuteReader();
            }
            return _reader.Read();
        }

        public int Depth
        {
            get { return _reader.Depth; }
        }

        public bool IsClosed
        {
            get { return _reader.IsClosed; }
        }

        public int RecordsAffected
        {
            get { return _reader.RecordsAffected; }
        }
    }
}