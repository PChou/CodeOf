using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace Algorithm
{
    public class OrderSync
    {
        public static void Sync(ISyncable Left, ISyncable Right)
        {
            if (Left == null || Right == null)
                return;
            IEnumerator lefter = Left.GetEnumerator();
            IEnumerator righter = Right.GetEnumerator();
            if (lefter == null || righter == null)
                return;

            IComparable a = null;//repesent the left current node
            IComparable b = null;//repesent the right current node
            if (lefter.MoveNext())
                a = (IComparable)lefter.Current;
            if (righter.MoveNext())
                b = (IComparable)righter.Current;

            while (a != null && b != null)
            {
                if (a.CompareTo(b) == 0)
                {
                    Left.DoEqualStep(a, b);
                    Right.DoEqualStep(b, a);
                    if (lefter.MoveNext() == false)
                    {
                        a = null;
                    }
                    else
                    {
                        a = (IComparable)lefter.Current;
                    }
                    if (righter.MoveNext() == false)
                    {
                        b = null;
                    }
                    else
                    {
                        b = (IComparable)righter.Current;
                    }
                }
                else if (a.CompareTo(b) > 0)
                {
                    Right.DoLessStep(b, a);
                    if (righter.MoveNext() == false)
                    {
                        b = null;
                    }
                    else
                    {
                        b = (IComparable)righter.Current;
                    }

                }
                else if (a.CompareTo(b) < 0)
                {
                    Left.DoLessStep(a, b);
                    if (lefter.MoveNext() == false)
                    {
                        a = null;
                    }
                    else
                    {
                        a = (IComparable)lefter.Current;
                    }

                }


            }

            if (a == null && b != null)
            {
                do
                {
                    b = (IComparable)righter.Current;
                    Right.DoMoreStep(b);
                }
                while (righter.MoveNext());
            }

            else if (b == null && a != null)
            {
                do
                {
                    a = (IComparable)lefter.Current;
                    Left.DoMoreStep(a);
                }
                while (lefter.MoveNext());
            }

        }
    }


    public interface ISyncable : IEnumerable
    {
        void DoEqualStep(IComparable Current, IComparable Another);
        void DoLessStep(IComparable Current,IComparable Another);
        void DoMoreStep(IComparable Current);
    }

    public class SqlCollection : ISyncable
    {
        protected SqlReader reader = null;

        public SqlCollection(SqlDataReader SqldataReader, GetComparable Getter)
        {
            reader = new SqlReader(SqldataReader,Getter);
        }

        public virtual void DoEqualStep(IComparable Current, IComparable Another)
        {
        }

        public virtual void DoLessStep(IComparable Current, IComparable Another)
        {
        }

        public virtual void DoMoreStep(IComparable Current)
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return reader;
        }

        public virtual IEnumerator GetEnumerator()
        {
            return reader;
        }
    }

    public class SqlReader : IEnumerator
    {
        SqlDataReader _reader = null;
        IComparable _Current = null;
        GetComparable _getter = null;

        public SqlReader(SqlDataReader reader, GetComparable getter)
        {
            _getter = getter;
            _reader = reader;
        }


        public object Current
        {
            get
            {
                _Current = _getter(_reader);
                return _Current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                _Current = _getter(_reader);
                return _Current;
            }
        }

        public bool MoveNext()
        {
            return _reader.Read();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    public delegate IComparable GetComparable(SqlDataReader r);

}
