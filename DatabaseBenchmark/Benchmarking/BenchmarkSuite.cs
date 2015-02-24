﻿using System;
using log4net;

namespace DatabaseBenchmark.Benchmarking
{
    /// <summary>
    /// Represents a benchmark test suite that executes and logs all of the tests.
    /// </summary>
    public class BenchmarkSuite
    {
        private ILog Logger;

        public event Action<BenchmarkTest, TestMethod> OnTestFinish;
        public BenchmarkTest Current { get; private set; }

        public BenchmarkSuite()
        {
            Logger = LogManager.GetLogger("BenchmarkTestLogger");
        }

        public void ExecuteInit(BenchmarkTest test)
        {
            Current = test;
            string databaseName = test.Database.DatabaseName;

            try
            {
                Logger.Info("");
                Logger.Info(String.Format("{0} Init() started...", databaseName));

                Current.Init();

                Logger.Info(String.Format("{0} Init() ended...", databaseName));
            }
            catch (Exception exc)
            {
                Logger.Error(String.Format("{0} Init()", databaseName), exc);
                Logger.Info(String.Format("{0} Init() failed...", databaseName), exc);
            }
            finally
            {
                Current = null;
            }
        }

        public void ExecuteWrite(BenchmarkTest test)
        {
            Current = test;
            string databaseName = test.Database.DatabaseName;

            try
            {
                Logger.Info(String.Format("{0} Write() started...", databaseName));
                Current.Write();
                Logger.Info(String.Format("{0} Write() ended...", databaseName));
            }
            catch (Exception exc)
            {
                Logger.Error(String.Format("{0} Write()", databaseName), exc);
                Logger.Info(String.Format("{0} Write() failed...", databaseName));
            }
            finally
            {
                Current = null;
                OnTestFinish(test, TestMethod.Write);
            }
        }

        public void ExecuteRead(BenchmarkTest test)
        {
            Current = test;
            string databaseName = test.Database.DatabaseName;

            try
            {
                Logger.Info(String.Format("{0} Read() started...", databaseName));
                Current.Read();
                Logger.Info(String.Format("{0} Read() ended...", databaseName));
            }
            catch (Exception exc)
            {
                Logger.Error(String.Format("{0} Read()", databaseName), exc);
                Logger.Info(String.Format("{0} Read() failed...", databaseName));
            }
            finally
            {
                Current = null;
                OnTestFinish(test, TestMethod.Read);
            }
        }

        public void ExecuteSecondaryRead(BenchmarkTest test)
        {
            Current = test;
            string databaseName = test.Database.DatabaseName;

            try
            {
                Logger.Info(String.Format("{0} SecondaryRead() started...", databaseName));
                Current.SecondaryRead();
                Logger.Info(String.Format("{0} SecondaryRead() ended...", databaseName));
            }
            catch (Exception exc)
            {
                Logger.Error(String.Format("{0} Secondary Read()", databaseName), exc);
                Logger.Info(String.Format("{0} Secondary Read failed...", databaseName));
            }
            finally
            {
                Current = null;
                OnTestFinish(test, TestMethod.SecondaryRead);
            }
        }

        public void ExecuteFinish(BenchmarkTest test)
        {
            Current = test;

            try
            {
                Current.Finish();
            }
            catch (Exception exc)
            {
                Logger.Error(String.Format("{0} Finish()", test.Database.DatabaseName), exc);
                Logger.Info(String.Format("{0} Finish() failed...", test.Database.DatabaseName));
            }
            finally
            {
                Current = null;
            }
        }
    }
}