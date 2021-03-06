﻿using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using NUnit.Framework;

namespace Atata.Tests
{
    public class Log4NetConsumerTests : UITestFixtureBase
    {
        private const string InfoLoggerName = "InfoLogger";

        private static FileInfo ConfigFileInfo =>
            new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config"));

        private static string LogsFolder =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "Log4Net");

        private static string TraceLogFilePath =>
            Path.Combine(LogsFolder, "Trace.log");

        public override void TearDown()
        {
            base.TearDown();

            foreach (var repository in log4net.LogManager.GetAllRepositories())
                repository.Shutdown();

            if (Directory.Exists(LogsFolder))
                Directory.Delete(LogsFolder, recursive: true);
        }

        [Test]
        public void Log4NetConsumer_Default()
        {
            XmlConfigurator.Configure(ConfigFileInfo);

            ConfigureBaseAtataContext().
                AddLog4NetLogging().
                Build();

            string traceTestMessage = Guid.NewGuid().ToString();
            string debugTestMessage = Guid.NewGuid().ToString();
            string infoTestMessage = Guid.NewGuid().ToString();

            AtataContext.Current.Log.Trace(traceTestMessage);
            AtataContext.Current.Log.Debug(debugTestMessage);
            AtataContext.Current.Log.Info(infoTestMessage);

            AssertThatFileShouldContainText(TraceLogFilePath, traceTestMessage, debugTestMessage, infoTestMessage);
        }

        [Test]
        public void Log4NetConsumer_WithRepositoryUsingInfoLevel()
        {
            var logRepository = log4net.LogManager.CreateRepository(Guid.NewGuid().ToString());
            XmlConfigurator.Configure(logRepository, ConfigFileInfo);

            ConfigureBaseAtataContext().
                AddLog4NetLogging(logRepository.Name, InfoLoggerName).
                Build();

            string traceTestMessage = Guid.NewGuid().ToString();
            string debugTestMessage = Guid.NewGuid().ToString();
            string infoTestMessage = Guid.NewGuid().ToString();

            AtataContext.Current.Log.Trace(traceTestMessage);
            AtataContext.Current.Log.Debug(debugTestMessage);
            AtataContext.Current.Log.Info(infoTestMessage);

            var fileAppenders = logRepository.GetAppenders().OfType<FileAppender>().ToArray();
            fileAppenders.Should().HaveCount(2);

            foreach (var fileAppender in fileAppenders)
            {
                AssertThatFileShouldContainText(fileAppender.File, infoTestMessage);
                AssertThatFileShouldNotContainText(fileAppender.File, traceTestMessage, debugTestMessage);
            }
        }

        [Test]
        public void Log4NetConsumer_WithMissingRepository()
        {
            string repositoryName = "MissingRepository";

            var exception = Assert.Throws<LogException>(() =>
                ConfigureBaseAtataContext().
                    AddLog4NetLogging(repositoryName, InfoLoggerName).
                    Build());

            exception.Message.Should().Be($"Repository [{repositoryName}] is NOT defined.");
        }

        [Test]
        public void Log4NetConsumer_WithUnconfiguredRepository()
        {
            var repository = log4net.LogManager.CreateRepository(Guid.NewGuid().ToString());

            ConfigureBaseAtataContext().
                AddLog4NetLogging(repository.Name, InfoLoggerName).
                Build();
        }
    }
}
