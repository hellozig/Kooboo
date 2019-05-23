﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kooboo.Dom;
using Kooboo.Sites.Service;

namespace Kooboo.Model.Render
{
    public class ElementWrap : NodeWrap
    {
        private Element _inner;

        public ElementWrap(DocumentWrap doc, Element el)
            : base(doc)
        {
            _inner = el;
        }

        public override Node Node => _inner;

        public IEnumerable<Attr> attributes => _inner.attributes;

        public string getAttribute(string name)
        {
            return _inner.getAttribute(name);
        }

        public void setAttribute(string name, string value)
        {
            _inner.setAttribute(name, value);

            Document.NotifyModified(this);
        }

        public void removeAttribute(string name)
        {
            _inner.removeAttribute(name);

            Document.NotifyModified(this);
        }

        public IEnumerable<ElementWrap> Select(string cssSelector)
        {
            return _inner.Select(cssSelector).item.Select(o => new ElementWrap(Document, o));
        }

        internal override int Output(StringBuilder sb)
        {
            sb.Append(DomService.GenerateOpenTag(_inner.attributes.ToDictionary(o => o.name, o => o.value), _inner.tagName));

            return Node.location.openTokenEndIndex + 1;
        }
    }
}
