﻿using System.Collections.ObjectModel;
using System.Reactive;
using ClashGui.Clash.Models.Providers;
using ClashGui.Clash.Models.Rules;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface IProxyRulesListViewModel: IViewModelBase
{
    ReadOnlyObservableCollection<RuleInfo> Rules { get; }
    ReadOnlyObservableCollection<RuleProvider> Providers { get; }
    
    ReactiveCommand<string, Unit> UpdateCommand { get; }
}