using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bennington.ContentTree.Contexts;
using MvcTurbine;
using MvcTurbine.Blades;

namespace Bennington.ContentTree.Cache
{
    public class ReigsterTreeNodeSummaryContextCacheBlade : Blade
    {
        public override void Spin(IRotorContext context)
        {
            var treeNodeSummaryContextCache = context.ServiceLocator.Resolve<TreeNodeSummaryContextCache>();
            context.ServiceLocator.Register<ITreeNodeSummaryContext>(treeNodeSummaryContextCache);
        }
    }
}
