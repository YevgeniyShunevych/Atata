﻿using System.Text;

namespace Atata
{
    public class FindByContentStrategy : SimpleElementFindStrategy
    {
        protected override void BuildXPath(StringBuilder builder, ElementFindOptions options)
        {
            builder.AppendFormat(
                "[{0} or {1}]",
                options.GetTermsXPathCondition(),
                options.GetTermsXPathCondition("@value"));
        }
    }
}