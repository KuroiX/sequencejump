﻿using System;

namespace SequenceJump.Player.Controller
{
    public sealed class Ref<T>
    {
        private readonly Func<T> _getter;
        private readonly Action<T> _setter;
        
        public Ref(Func<T> getter, Action<T> setter)
        {
            _getter = getter;
            _setter = setter;
        }
        
        public T Value { 
            get => _getter();
            set => _setter(value);
        }
    }
}