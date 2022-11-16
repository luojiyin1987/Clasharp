﻿using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ClashGui.Cli;

namespace ClashGui.Services.Base;

public abstract class BaseService<T> : IAutoFreshable
{
    public bool EnableAutoFresh { get; set; } = true;
    protected IClashApiFactory _clashApiFactory;
    protected IClashCli _clashCli;

    protected BaseService(IClashApiFactory clashApiFactory, IClashCli clashCli)
    {
        _clashApiFactory = clashApiFactory;
        _clashCli = clashCli;
    }

    protected abstract Task<T> GetObj();

    protected IObservable<T> GetObservable()
    {
        return Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .CombineLatest(_clashCli.RunningState)
            .Where(tuple => tuple.Second == RunningState.Started && EnableAutoFresh)
            .SelectMany(_ => GetObj());
    }
}