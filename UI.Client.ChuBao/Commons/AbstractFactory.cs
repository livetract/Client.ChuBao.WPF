﻿using System;

namespace UI.Client.ChuBao.Commons
{
    public class AbstractFactory<T> : IAbstractFactory<T>
    {
        private readonly Func<T> _factory;

        public AbstractFactory(Func<T> factory)
        {
            this._factory = factory;
        }
        public T Create()
        {
            return _factory();
        }

    }
}