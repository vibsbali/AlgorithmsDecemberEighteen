using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Graphs
{
   public interface IGraphSearch
   {
      IGraph Graph { get; }
      bool AreConnected(int start, int goal);
      List<int> GetPath();
   }
}
