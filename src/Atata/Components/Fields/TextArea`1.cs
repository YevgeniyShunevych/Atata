﻿using System;
using OpenQA.Selenium;

namespace Atata
{
    /// <summary>
    /// Represents the text area control (<c>&lt;textarea&gt;</c>).
    /// Default search is performed by the label.
    /// </summary>
    /// <typeparam name="TOwner">The type of the owner page object.</typeparam>
    [ControlDefinition("textarea", IgnoreNameEndings = "TextArea", ComponentTypeName = "text area")]
    [FindByLabel]
    public class TextArea<TOwner> : EditableTextField<string, TOwner>
        where TOwner : PageObject<TOwner>
    {
        /// <summary>
        /// Appends the specified value.
        /// Also executes <see cref="TriggerEvents.BeforeSet" /> and <see cref="TriggerEvents.AfterSet" /> triggers.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The owner page object.</returns>
        [Obsolete("Use Type(...) instead.")] // Obsolete since v1.9.0.
        public TOwner Append(string value)
        {
            ExecuteTriggers(TriggerEvents.BeforeSet);

            Log.ExecuteSection(
                new ValueChangeLogSection(this, "Append", value),
                () => Scope.SendKeysWithLogging(Keys.End + value));

            ExecuteTriggers(TriggerEvents.AfterSet);

            return Owner;
        }
    }
}
