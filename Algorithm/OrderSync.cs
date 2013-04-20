using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm
{
    public class OrderSync
    {
        public static void Sync<T>(IEnumerable<T> Left, IEnumerable<T> Right) where T : Syncable
        {
            if (Left == null || Right == null)
                return;
            IEnumerator<T> lefter = Left.GetEnumerator();
            IEnumerator<T> righter = Right.GetEnumerator();
            if (lefter == null || righter == null)
                return;

            T a = null;//repesent the left current node
            T b = null;//repesent the right current node
            if (lefter.MoveNext())
                a = lefter.Current;
            if (righter.MoveNext())
                b = righter.Current;

            while (a != null && b != null)
            {
                if (a.CompareTo(b) == 0)
                {
                    a.DoStepSameTimeLeft(b);
                    b.DoStepSameTimeRight(a);
                    if (lefter.MoveNext() == false)
                    {
                        a = null;
                    }
                    else
                    {
                        a = lefter.Current;
                    }
                    if (righter.MoveNext() == false)
                    {
                        b = null;
                    }
                    else
                    {
                        b = righter.Current;
                    }
                }
                else if (a.CompareTo(b) > 0)
                {
                    b.DoRightStep(a);
                    if (righter.MoveNext() == false)
                    {
                        b = null;
                    }
                    else
                    {
                        b = righter.Current;
                    }

                }
                else if (a.CompareTo(b) < 0)
                {
                    a.DoLeftStep(b);
                    if (lefter.MoveNext() == false)
                    {
                        a = null;
                    }
                    else
                    {
                        a = lefter.Current;
                    }

                }


            }

            if (a == null && b != null)
            {
                do
                {
                    b = righter.Current;
                    b.DoMoreRight();
                }
                while (righter.MoveNext());
            }

            else if (b == null && a != null)
            {
                do
                {
                    a = lefter.Current;
                    a.DoMoreLeft();
                }
                while (lefter.MoveNext());
            }

        }
    }

    public abstract class Syncable
    {
        public abstract int CompareTo(Syncable Another);
        public abstract void DoStepSameTimeLeft(Syncable Equaler);
        public abstract void DoStepSameTimeRight(Syncable Equaler);
        public abstract void DoLeftStep(Syncable LessThan);
        public abstract void DoRightStep(Syncable LessThan);
        public abstract void DoMoreLeft();
        public abstract void DoMoreRight();
    }
}
