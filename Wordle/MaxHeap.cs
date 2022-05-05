using System;
using System.Linq;

namespace Wordle
{
	public class MaxHeap<T> 
	{
		private (int amount, T name)[]array;
		private int initialSize = 8;

		public int Count { get; private set; } = 0;

		public int Capacity => array.Length;

		public bool IsEmpty => Count == 0;

		public MaxHeap()
		{
			array = new (int amount, T name)[initialSize];
		}

		/// <summary>
		/// Returns the max item but does NOT remove it.
		/// Time complexity: O(1).
		/// </summary>
		public (int amount, T name) Peek()
        {
			if (IsEmpty)
			{
				throw new Exception("Empty Heap");
			}

			return array[0];
        }

		/// <summary>
		/// Adds given item to the heap.
		/// Time complexity: O(1).
		/// </summary>
		public void Add((int amount, T name) item)
        {
			if (Containsname(item.name))
            {
				for (int i = 0; i < Count; i++)
                {
					if (item.name.Equals(array[i].name))
                    {
						array[i].amount += item.amount;
						TrickleUp(i);
                    }
                }
            }
			else
            {
				int nextEmptyIndex = Count;

				array[nextEmptyIndex] = item;

				TrickleUp(Count);

				Count++;

				// Resize array if full
				if (Count == array.Length)
				{
					DoubleArrayCapacity();
				}
			}
		}

		public (int amount, T name) Extract()
        {
			return ExtractMax();
        }

		// TODO
		/// <summary>
		/// Removes and returns the max item in the max-heap.
		/// Time complexity: O(log(N)).
		/// </summary>
		public (int amount, T name) ExtractMax()
        {
			if (IsEmpty)
			{
				throw new Exception("Empty Heap");
			}

			(int amount, T name) max = array[0];

			// swap max with last element
			Swap(0, Count - 1);

			// remove last element
			Count--;

			// trickle down from root
			TrickleDown(0);

			return max;
        }

		/// <summary>
		/// Removes and returns the min item in the max-heap.
		/// Time complexity: O(N + log(N)).
		/// </summary>
		public (int amount, T name) ExtractMin()
        {
			if(IsEmpty)
            {
				throw new Exception("Empty Heap");
            }

			int minIndex = (Count-1)/2 + 1;
			(int amount, T name) min = array[minIndex];
			for (int i = minIndex + 1; i < Count; i++)
            {
				if (array[i].amount.CompareTo(min.amount) < 0)
                {
					min.amount = array[i].amount;
					minIndex = i;
                }
            }

			// remove min
			// swap min with last element
			Swap(minIndex, Count - 1);

			// remove last element
			Count--;

			TrickleUp(minIndex);

			return min;
		}

		/// <summary>
		/// Returns true if the heap contains the given value; otherwise false.
		/// Time complexity: O(N).
		/// </summary>
		public bool ContainsAmt(int name)
		{
			for (int i=0; i < Count; i++)
            {
				if (array[i].amount.Equals(name))
                {
					return true;
                }
            }
			
			return false;
		}

		public bool Containsname(T name)
        {
			for (int i=0; i < Count; i++)
            {
				if (array[i].name.Equals(name))
                {
					return true;
                }
            }

			return false;
        }

		// TODO
		private void TrickleUp(int index)
		{
			if (array[index].amount.CompareTo(array[Parent(index)].amount) > 0)
            {
				Swap(index, Parent(index));
				TrickleUp(Parent(index));
            }
		}

		// TODO
		private void TrickleDown(int index)
		{
			if (LeftChild(index) >= Count)
			{
				return;
			}
			if (array[index].amount.CompareTo(array[LeftChild(index)].amount) < 0 && LeftChild(index) < Count)
            {
				if (array[LeftChild(index)].CompareTo(array[RightChild(index)]) > 0 || RightChild(index) >= Count)
                {
					Swap(index, LeftChild(index));
					TrickleDown(LeftChild(index));
                }
            }
			if (array[index].amount.CompareTo(array[RightChild(index)].amount) < 0 && RightChild(index) < Count)
            {
				if (array[RightChild(index)].amount.CompareTo(array[LeftChild(index)].amount) > 0)
                {
					Swap(index, RightChild(index));
					TrickleDown(RightChild(index));
                }
            }
		}

		/// <summary>
		/// Gives the position of a node's parent, the node's position in the heap.
		/// </summary>
		// TODO
		private static int Parent(int position)
		{
			return (position - 1) / 2;
		}

		/// <summary>
		/// Returns the position of a node's left child, given the node's position.
		/// </summary>
		// TODO
		private static int LeftChild(int position)
		{
			return position * 2 + 1;
		}

		/// <summary>
		/// Returns the position of a node's right child, given the node's position.
		/// </summary>
		// TODO
		private static int RightChild(int position)
		{
			return position * 2 + 2;
		}

		private void Swap(int index1, int index2)
		{
			var temp = array[index1];

			array[index1] = array[index2];
			array[index2] = temp;
		}

		private void DoubleArrayCapacity()
		{
			Array.Resize(ref array, array.Length * 2);
		}
	}
}

