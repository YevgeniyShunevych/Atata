﻿using Humanizer;
using System;

namespace Atata
{
    public class ElementFindOptions : ICloneable
    {
        public string[] Terms { get; set; }
        public string ElementXPath { get; set; }
        public string IdFinderFormat { get; set; }
        public int? Index { get; set; }
        public TermMatch Match { get; set; }
        public bool IsSafely { get; set; }

        public bool HasIndex
        {
            get { return Index.HasValue; }
        }

        public int? Position
        {
            get { return HasIndex ? Index + 1 : null; }
        }

        public string GetTermsXPathCondition(string value = ".")
        {
            return Match.CreateXPathCondition(Terms, value);
        }

        public string GetTermsAsString()
        {
            return Terms != null ? string.Join("/", Terms) : null;
        }

        public string GetPositionWrappedXPathCondition()
        {
            return HasIndex ? "[{0}]".FormatWith(Position) : null;
        }

        public ElementFindOptions Clone()
        {
            return (ElementFindOptions)MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return MemberwiseClone();
        }
    }
}