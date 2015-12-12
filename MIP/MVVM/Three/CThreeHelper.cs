using MIP.MVVM.ViewModelsBase.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIP.MVVM.ViewModelsBase.Collections//MIP.MVVM.Three
{
	public class CThreeHelper<TItem> where TItem : NodeViewModel<TItem>
	{

		/*private static NodeViewModel<TItem> GetSelectedItem(IEnumerable<NodeViewModel<TItem>> items)
		{
			//top-level items:
			NodeViewModel<TItem> item = items.FirstOrDefault(i => i.IsSelected);
			if (item == null)
			{
				//sub-level items:
				IEnumerable<NodeViewModel<TItem>> subItems = items.OfType<NodeViewModel<TItem>>().SelectMany(d => d.Children);
				if (items.Any())
					item = GetSelectedItem(subItems);
			}
			return item;
		}*/

		public static NodeViewModel<TItem> GetSelectedItem(ObservableCollection<NodeViewModel<TItem>> nodes)
		{
			NodeViewModel<TItem> selectedNode = null;
			foreach (var node in nodes)
			{
				selectedNode = GetSelected(node);
				if (selectedNode != null)
					return selectedNode;
			}
			return selectedNode;
		}

		private static NodeViewModel<TItem> GetSelected(NodeViewModel<TItem> baseNode)
		{
			if (baseNode == null) 
				return null;

			if (baseNode.IsSelected)
				return baseNode;

			if (baseNode.Children != null)
				foreach (var node in baseNode.Children)
				{
					if (node != null && node.IsSelected)
						return node;
					if (node.Children != null)
					{
						var temp = GetSelected(node);
						if (temp != null) return temp;
					}
				}

			return null;
		}

		public static void RemoveRecursive(ObservableCollection<NodeViewModel<TItem>> nodes, NodeViewModel<TItem> target)
		{
			bool result = nodes.Remove(target);
			if (!result)
			{
				foreach (var node in nodes)
					RemoveSelected(node, target);
			}
		}

		private static NodeViewModel<TItem> RemoveSelected(NodeViewModel<TItem> baseNode, NodeViewModel<TItem> target)
		{
			if (baseNode == null) 
				return null;

			if (baseNode.Children != null)
			{
				if (baseNode.Children.Remove(target))
					return null;

				foreach (NodeViewModel<TItem> node in baseNode.Children)
				{

					if (node.Children != null)
						return RemoveSelected(node, target);
				}
			}
			return null;
		}

	}
}
