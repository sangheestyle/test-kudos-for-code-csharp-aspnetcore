﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Microsoft.AspNetCore.Http.Generators.Tests;

namespace Microsoft.AspNetCore.Http.Microbenchmarks;

[MemoryDiagnoser]
[EventPipeProfiler(EventPipeProfile.GcVerbose)]
public class RequestDelegateGeneratorBenchmarks : RequestDelegateCreationTestBase
{
    protected override bool IsGeneratorEnabled => true;

    [Params(10, 100, 1000, 20000)]
    public int EndpointCount { get; set; }

    [Benchmark]
    public async Task CreateRequestDelegate()
    {
        var source = "";
        for (var i = 0; i < EndpointCount; i++)
        {
            source += $"""app.MapGet("/route{i}", (int? id) => "Hello World!");""";
        }
        await RunGeneratorAsync(source);
    }
}
