using RedPanda.Project.Services.Interfaces;
using System;

namespace RedPanda.Project.Services
{
    public sealed class UserService : IUserService
    {
        public int Currency { get; private set; }
        public event Action<int> OnUpdateCurrency;

        public UserService()
        {
            Currency = 1000;
        }

        void IUserService.AddCurrency(int delta)
        {
            Currency += delta;
            OnUpdateCurrency?.Invoke(Currency);
        }

        void IUserService.ReduceCurrency(int delta)
        {
            Currency -= delta;
            OnUpdateCurrency?.Invoke(Currency);
        }
        
        bool IUserService.HasCurrency(int amount)
        {
            return Currency >= amount;
        }
    }
}