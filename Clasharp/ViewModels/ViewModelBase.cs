﻿using System;
using System.Reactive;
using Clasharp.Interfaces;
using ReactiveUI;

namespace Clasharp.ViewModels
{
    public class ViewModelBase : ReactiveObject, IViewModelBase
    {
        public virtual string Name { get; }

        public Interaction<(Exception, bool), Unit> ShowError { get; } = new();

    }
}