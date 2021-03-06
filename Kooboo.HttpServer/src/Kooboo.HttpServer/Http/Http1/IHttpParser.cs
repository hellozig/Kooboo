// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Buffers;
using System.Collections;
using System.Collections.Sequences;
using System.IO.Pipelines;

namespace Kooboo.HttpServer.Http
{
    public interface IHttpParser<TRequestHandler> where TRequestHandler : IHttpHeadersHandler, IHttpRequestLineHandler
    {
        bool ParseRequestLine(TRequestHandler handler, ReadOnlyBuffer<byte> buffer, out SequencePosition consumed, out SequencePosition examined);

        bool ParseHeaders(TRequestHandler handler, ReadOnlyBuffer<byte> buffer, out SequencePosition consumed, out SequencePosition examined, out int consumedBytes);
    }
}
