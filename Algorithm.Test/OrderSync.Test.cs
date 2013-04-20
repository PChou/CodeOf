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
            SyncableString.store.ClearAll();
            List<SyncableString> left = SyncableString.ConstructList("a", "b", "c", "g", "i", "j");
            List<SyncableString> right = SyncableString.ConstructList("a", "b", "h", "i", "k", "m","p","z");
            OrderSync.Sync<SyncableString>(left, right);
            Assert.AreEqual(3, SyncableString.store.LeftEqual.Count);//abi
            Assert.AreEqual(3, SyncableString.store.rightEqual.Count);//abi
            Assert.AreEqual(3, SyncableString.store.LeftStep.Count);//cgj
            Assert.AreEqual(1, SyncableString.store.rightStep.Count);//h
            Assert.AreEqual(0, SyncableString.store.LeftMore.Count);
            Assert.AreEqual(4, SyncableString.store.rightMore.Count);//kmpz
        }

        [TestMethod]
        public void OrderSync_Test_02()
        {
            SyncableString.store.ClearAll();
            List<SyncableString> left = SyncableString.ConstructList(null);
            List<SyncableString> right = SyncableString.ConstructList("a", "b", "h", "i", "k", "m", "p", "z");
            OrderSync.Sync<SyncableString>(left, right);
            Assert.AreEqual(0, SyncableString.store.LeftEqual.Count);
            Assert.AreEqual(0, SyncableString.store.rightEqual.Count);
            Assert.AreEqual(0, SyncableString.store.LeftStep.Count);
            Assert.AreEqual(0, SyncableString.store.rightStep.Count);
            Assert.AreEqual(0, SyncableString.store.LeftMore.Count);
            Assert.AreEqual(8, SyncableString.store.rightMore.Count);
        }

        [TestMethod]
        public void OrderSync_Test_03()
        {
            SyncableString.store.ClearAll();
            List<SyncableString> left = SyncableString.ConstructList("a", "b", "c", "g", "i", "j");
            List<SyncableString> right = SyncableString.ConstructList(null);
            OrderSync.Sync<SyncableString>(left, right);
            Assert.AreEqual(0, SyncableString.store.LeftEqual.Count);
            Assert.AreEqual(0, SyncableString.store.rightEqual.Count);
            Assert.AreEqual(0, SyncableString.store.LeftStep.Count);
            Assert.AreEqual(0, SyncableString.store.rightStep.Count);
            Assert.AreEqual(6, SyncableString.store.LeftMore.Count);
            Assert.AreEqual(0, SyncableString.store.rightMore.Count);
        }


        [TestMethod]
        public void OrderSync_Test_04()
        {
            SyncableString.store.ClearAll();
            List<SyncableString> left = SyncableString.ConstructList("a", "b", "c", "g", "i", "j");
            List<SyncableString> right = SyncableString.ConstructList("a", "b", "c", "g", "i", "j");
            OrderSync.Sync<SyncableString>(left, right);
            Assert.AreEqual(6, SyncableString.store.LeftEqual.Count);
            Assert.AreEqual(6, SyncableString.store.rightEqual.Count);
            Assert.AreEqual(0, SyncableString.store.LeftStep.Count);
            Assert.AreEqual(0, SyncableString.store.rightStep.Count);
            Assert.AreEqual(0, SyncableString.store.LeftMore.Count);
            Assert.AreEqual(0, SyncableString.store.rightMore.Count);
        }

        [TestMethod]
        public void OrderSync_Test_05()
        {
            SyncableString.store.ClearAll();
            List<SyncableString> left = SyncableString.ConstructList("a", "b", "c", "g", "i", "j");
            List<SyncableString> right = SyncableString.ConstructList("a", "b", "c", "g", "i", "k");
            OrderSync.Sync<SyncableString>(left, right);
            Assert.AreEqual(5, SyncableString.store.LeftEqual.Count);
            Assert.AreEqual(5, SyncableString.store.rightEqual.Count);
            Assert.AreEqual(1, SyncableString.store.LeftStep.Count);
            Assert.AreEqual(0, SyncableString.store.rightStep.Count);
            Assert.AreEqual(0, SyncableString.store.LeftMore.Count);
            Assert.AreEqual(1, SyncableString.store.rightMore.Count);
        }

        [TestMethod]
        public void OrderSync_Test_06()
        {
            SyncableString.store.ClearAll();
            List<SyncableString> left = SyncableString.ConstructList(null);
            List<SyncableString> right = SyncableString.ConstructList(null);
            OrderSync.Sync<SyncableString>(left, right);
            Assert.AreEqual(0, SyncableString.store.LeftEqual.Count);
            Assert.AreEqual(0, SyncableString.store.rightEqual.Count);
            Assert.AreEqual(0, SyncableString.store.LeftStep.Count);
            Assert.AreEqual(0, SyncableString.store.rightStep.Count);
            Assert.AreEqual(0, SyncableString.store.LeftMore.Count);
            Assert.AreEqual(0, SyncableString.store.rightMore.Count);
        }
    }


    public class Store
    {
        public List<SyncableString> LeftEqual = new List<SyncableString>();
        public List<SyncableString> rightEqual = new List<SyncableString>();
        public List<SyncableString> LeftStep = new List<SyncableString>();
        public List<SyncableString> rightStep = new List<SyncableString>();
        public List<SyncableString> LeftMore = new List<SyncableString>();
        public List<SyncableString> rightMore = new List<SyncableString>();

        public void ClearAll()
        {
            LeftEqual.Clear();
            rightEqual.Clear();
            LeftStep.Clear();
            rightStep.Clear();
            LeftMore.Clear();
            rightMore.Clear();
        }
    }


    public class SyncableString : Syncable
    {
        public static List<SyncableString> ConstructList(params string[] param)
        {
            List<SyncableString> result = new List<SyncableString>();
            if (param == null)
                return result;
            foreach (string s in param)
            {
                result.Add(new SyncableString(s));
            }

            return result;
        }

        public static Store store = new Store();

        private String _InnerString;

        public String InnerString { 
            get{
                return _InnerString;
            }
            set {
                _InnerString = value;
            }
        }

        public SyncableString(String input)
        {
            _InnerString = input;
        }



        public override int CompareTo(Syncable Another)
        {
            if (Another is SyncableString)
                return this.InnerString.CompareTo(((SyncableString)Another).InnerString);
            else
                throw new ArgumentException("Another must be SyncableString");
        }

        public override void DoMoreLeft()
        {
            store.LeftMore.Add(this);
        }

        public override void DoMoreRight()
        {
            store.rightMore.Add(this);
        }

        public override void DoStepSameTimeLeft(Syncable Equaler)
        {
            store.LeftEqual.Add(this);
        }

        public override void DoStepSameTimeRight(Syncable Equaler)
        {
            store.rightEqual.Add(this);
        }

        public override void DoLeftStep(Syncable LessThan)
        {
            store.LeftStep.Add(this);
        }

        public override void DoRightStep(Syncable LessThan)
        {
            store.rightStep.Add(this);
        }
    }




}
