using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Algorithm;

namespace Algorithm.Test
{
    [TestClass]
    public class OrderSync_Test
    {
        [TestMethod]
        public void OrderSync_Test_01()
        {
            StringSynCollection left = new StringSynCollection("a", "b", "c", "g", "i", "j");
            StringSynCollection right = new StringSynCollection("a", "b", "h", "i", "k", "m", "p", "z");
            OrderSync.Sync(left, right);
            Assert.AreEqual(3, left.Equal.Count);//abi
            Assert.AreEqual(3, right.Equal.Count);//abi
            Assert.AreEqual(3, left.Less.Count);//cgj
            Assert.AreEqual(1, right.Less.Count);//h
            Assert.AreEqual(0, left.More.Count);
            Assert.AreEqual(4, right.More.Count);//kmpz
        }

        [TestMethod]
        public void OrderSync_Test_02()
        {
            StringSynCollection left = new StringSynCollection(null);
            StringSynCollection right = new StringSynCollection("a", "b", "h", "i", "k", "m", "p", "z");
            OrderSync.Sync(left, right);
            Assert.AreEqual(0, left.Equal.Count);
            Assert.AreEqual(0, right.Equal.Count);
            Assert.AreEqual(0, left.Less.Count);
            Assert.AreEqual(0, right.Less.Count);
            Assert.AreEqual(0, left.More.Count);
            Assert.AreEqual(8, right.More.Count);
        }

        [TestMethod]
        public void OrderSync_Test_03()
        {
            StringSynCollection left = new StringSynCollection("a", "b", "c", "g", "i", "j");
            StringSynCollection right = new StringSynCollection(null);
            OrderSync.Sync(left, right);
            Assert.AreEqual(0, left.Equal.Count);
            Assert.AreEqual(0, right.Equal.Count);
            Assert.AreEqual(0, left.Less.Count);
            Assert.AreEqual(0, right.Less.Count);
            Assert.AreEqual(6, left.More.Count);
            Assert.AreEqual(0, right.More.Count);
        }


        [TestMethod]
        public void OrderSync_Test_04()
        {
            StringSynCollection left = new StringSynCollection("a", "b", "c", "g", "i", "j");
            StringSynCollection right = new StringSynCollection("a", "b", "c", "g", "i", "j");
            OrderSync.Sync(left, right);
            Assert.AreEqual(6, left.Equal.Count);
            Assert.AreEqual(6, right.Equal.Count);
            Assert.AreEqual(0, left.Less.Count);
            Assert.AreEqual(0, right.Less.Count);
            Assert.AreEqual(0, left.More.Count);
            Assert.AreEqual(0, right.More.Count);
        }

        [TestMethod]
        public void OrderSync_Test_05()
        {
            StringSynCollection left = new StringSynCollection("a", "b", "c", "g", "i", "j");
            StringSynCollection right = new StringSynCollection("a", "b", "c", "g", "i", "k");
            OrderSync.Sync(left, right);
            Assert.AreEqual(5, left.Equal.Count);
            Assert.AreEqual(5, right.Equal.Count);
            Assert.AreEqual(1, left.Less.Count);
            Assert.AreEqual(0, right.Less.Count);
            Assert.AreEqual(0, left.More.Count);
            Assert.AreEqual(1, right.More.Count);
        }

        [TestMethod]
        public void OrderSync_Test_06()
        {
            StringSynCollection left = new StringSynCollection(null);
            StringSynCollection right = new StringSynCollection(null);
            OrderSync.Sync(left, right);
            Assert.AreEqual(0, left.Equal.Count);
            Assert.AreEqual(0, right.Equal.Count);
            Assert.AreEqual(0, left.Less.Count);
            Assert.AreEqual(0, right.Less.Count);
            Assert.AreEqual(0, left.More.Count);
            Assert.AreEqual(0, right.More.Count);
        }

    }


    public class StringSynCollection : ISyncable
    {
        private List<string> _inner = null;

        public StringSynCollection(params string[] nodes)
        {
            _inner = new List<string>();
            if (nodes != null)
            {
                foreach (var n in nodes)
                {
                    _inner.Add(n);
                }
            }
        }

        public List<string> Equal = new List<string>();
        public List<string> Less = new List<string>();
        public List<string> More = new List<string>();

        public void DoEqualStep(IComparable Current, IComparable Another)
        {
            Equal.Add((string)Current);
        }

        public void DoLessStep(IComparable Current, IComparable Another)
        {
            Less.Add((string)Current);
        }

        public void DoMoreStep(IComparable Current)
        {
            More.Add((string)Current);
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _inner.GetEnumerator();
        }
    }





}
