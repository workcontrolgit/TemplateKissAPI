using $ext_projectname$.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace $safeprojectname$.Repository
{
    public class MockRepository<T> : IMockRepository<T>
        where T : class, new()
    {
        /// <summary>
        /// Generates a collection of type T based on the properties in T
        /// </summary>
        /// <returns>List<T></returns>
        public async Task<IEnumerable<T>> Collection()
        {
            return await Task.Run(() => GenFu.GenFu.ListOf<T>());
        }

        /// <summary>
        /// Generates the collection of type T of size = length 
        /// </summary>
        /// <param name="length">The size of the collection to be passed</param>
        /// <returns>A collection of type T based on the length passed</returns>
        public async Task<IEnumerable<T>> Collection(int length)
        {
            return await Task.Run(() => (GenFu.GenFu.ListOf<T>(length)));
        }

        /// <summary>
        /// Generates an object of type T with data
        /// </summary>
        /// <returns>T with data based on the properties in T</returns>
        public async Task<T> Instance()
        {
            return await Task.Run(() => GenFu.GenFu.New<T>());
        }
    }
}
