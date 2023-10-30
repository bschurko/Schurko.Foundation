using System;
using System.Threading.Tasks;

namespace VivaVictoria.Retask.Interfaces
{
    public interface IJob
    {
        public DateTime NextRun(DateTime last);
        public Task Do();
    }
}
