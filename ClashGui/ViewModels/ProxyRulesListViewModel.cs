﻿using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using ClashGui.Clash.Models.Providers;
using ClashGui.Clash.Models.Rules;
using ClashGui.Interfaces;
using ClashGui.Services;
using DynamicData;
using ReactiveUI;

namespace ClashGui.ViewModels;

public class ProxyRulesListViewModel : ViewModelBase, IProxyRulesListViewModel
{
    public override string Name => "Rules";

    public ProxyRulesListViewModel(IProxyRuleProviderService proxyRuleProviderService,
        IProxyRuleService proxyRuleService)
    {
        proxyRuleProviderService.List.ObserveOn(RxApp.MainThreadScheduler).Bind(out _ruleProviders).Subscribe();
        proxyRuleService.List.ObserveOn(RxApp.MainThreadScheduler).Bind(out _ruleInfos).Subscribe();

        UpdateCommand = ReactiveCommand.CreateFromTask<string>(async name =>
        {
            await proxyRuleService.UpdateRuleProvider(name);
        });
    }

    // [ObservableAsProperty]
    public ReadOnlyObservableCollection<RuleInfo> Rules => _ruleInfos;

    private ReadOnlyObservableCollection<RuleInfo> _ruleInfos;

    // [ObservableAsProperty]
    public ReadOnlyObservableCollection<RuleProvider> Providers => _ruleProviders;
    private ReadOnlyObservableCollection<RuleProvider> _ruleProviders;

    public ReactiveCommand<string, Unit> UpdateCommand { get; }
}