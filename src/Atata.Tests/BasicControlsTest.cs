﻿using NUnit.Framework;

namespace Atata.Tests
{
    [TestFixture]
    public class BasicControlsTest : TestBase
    {
        [Test]
        public void BasicControlsInteraction()
        {
            string firstName;

            Go.To<BasicControlsPage>().
                Header.VerifyEquals("Basic Controls").

                ByLabel.FirstName.VerifyExists().
                ByLabel.FirstName.VerifyEnabled().
                ByLabel.FirstName.VerifyIsNotReadOnly().
                ByLabel.FirstName.SetRandom(out firstName).
                ByLabel.FirstName.Should.Equal(firstName).
                ById.FirstName.Should.Equal(firstName).

                ByLabel.LastName.Set("LastName").
                ByLabel.LastName.Should.Equal("LastName").
                ById.LastName.Should.Equal("LastName").

                ById.MiddleName.Set("mdname").
                Do(_ => _.ByLabel.MiddleName, x =>
                    {
                        x.Should.Equal("mdname");
                        x.Set("md2name");
                        x.Should.Not.Equal("mdname");
                    }).
                ById.MiddleName.Should.Equal("md2name").

                ByLabel.ReadonlyField.VerifyExists().
                ByLabel.ReadonlyField.VerifyEnabled().
                ByLabel.ReadonlyField.VerifyIsReadOnly().
                ByLabel.ReadonlyField.Should.Equal("readme").

                ByLabel.DisabledField.VerifyExists().
                ByLabel.DisabledField.VerifyDisabled().
                ByLabel.DisabledField.VerifyIsNotReadOnly().
                ByLabel.DisabledField.Should.Equal("readme").

                RawButtonControl.VerifyExists().
                RawButtonControl.VerifyEnabled().
                RawButtonControl.Content.Should.Equal("Raw Button").
                RawButtonControl.Click().
                InputButtonControl.VerifyExists().
                InputButtonControl.Click().
                Do(_ => _.LinkButtonControl, x =>
                    {
                        x.VerifyExists();
                        x.Content.Should.Equal("Link Button");
                        x.Content.Should.StartWith("Link");
                        x.Content.Should.EndWith("utton");
                        x.Content.Should.Contain("ink Butto");
                        x.Content.Should.ContainIgnoringCase("k but");
                        x.Click();
                    }).
                ClickableControl.VerifyExists().
                ClickableControl.Click().
                DisabledButtonControl.VerifyExists().
                DisabledButtonControl.VerifyDisabled().
                MissingButtonControl.VerifyMissing();
        }
    }
}
