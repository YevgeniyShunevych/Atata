﻿namespace Atata
{
    /// <summary>
    /// Indicates that the component should be verified whether it is missing on the specified event. By default occurs upon the page object initialization.
    /// </summary>
    public class VerifyMissingAttribute : TriggerAttribute
    {
        public VerifyMissingAttribute(TriggerEvents on = TriggerEvents.Init, TriggerPriority priority = TriggerPriority.Medium)
            : base(on, priority)
        {
        }

        public override void Execute<TOwner>(TriggerContext<TOwner> context)
        {
            context.Component.Should.Not.Exist();
        }
    }
}
