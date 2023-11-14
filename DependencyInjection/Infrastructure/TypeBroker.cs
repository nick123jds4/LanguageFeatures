using DependencyInjection.Data;
using DependencyInjection.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Infrastructure
{
    /// <summary>
    /// Брокер типов
    /// </summary>
    public static class TypeBroker
    {
        private static Type _repoType = typeof(MemoryRepository);//значение по умолчанию
        private static IRepository _repository;
        public static IRepository Repository => _repository ?? Activator.CreateInstance(_repoType) as IRepository;

        /// <summary>
        /// Заменить значение по умолчанию на свой тип
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void SetRepositoryType<T>() where T: IRepository {
            _repoType = typeof(T);
        }
        /// <summary>
        /// Только для модульных тестов
        /// </summary>
        /// <param name="repository"></param>
        public static void SetTestObject(IRepository repository) {
            _repository = repository;
        }

    }
}
