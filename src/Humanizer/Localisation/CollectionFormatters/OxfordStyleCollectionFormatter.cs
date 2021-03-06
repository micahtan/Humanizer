﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Humanizer.Localisation.CollectionFormatters
{
    internal class OxfordStyleCollectionFormatter : DefaultCollectionFormatter
    {
        public OxfordStyleCollectionFormatter(string defaultSeparator)
            : base(defaultSeparator ?? "and")
        {
        }

        public override string Humanize<T>(IEnumerable<T> collection, Func<T, string> objectFormatter, string separator)
        {
            if (collection == null)
                throw new ArgumentException("collection");

            var enumerable = collection as T[] ?? collection.ToArray();

            var count = enumerable.Count();

            if (count == 0)
                return "";

            if (count == 1)
                return objectFormatter(enumerable.First());

            var formatString = count > 2 ? "{0}, {1} {2}" : "{0} {1} {2}";

            return string.Format(formatString,
                string.Join(", ", enumerable.Take(count - 1).Select(objectFormatter)),
                separator,
                objectFormatter(enumerable.Skip(count - 1).First()));
        }
    }
}