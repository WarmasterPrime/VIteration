namespace VIteration
{
	/// <summary>
	/// Provides lambda iteration extension methods for classes that inherit the <see cref="IEnumerable{T}"/> interface.
	/// </summary>
	public static class VIterationExt
	{
		/// <summary>
		/// Iterates through the <paramref name="source"/> and performs a process for each item within the array.
		/// </summary>
		/// <typeparam name="TIn">The input data-type.</typeparam>
		/// <typeparam name="TOut">The output data-type.</typeparam>
		/// <param name="source">The source array to iterate through.</param>
		/// <param name="predicate">The predicate that will be performed on for each item in the <paramref name="source"/> array.</param>
		/// <returns>an array of the results.</returns>
		/// <exception cref="Exception"></exception>
		public static TOut[] Iterate<TIn, TOut>(this IEnumerable<TIn> source, Func<TIn, TOut> predicate)
		{
			if(source is null)
				return Array.Empty<TOut>();
			if(predicate is null)
				throw new Exception("Function cannot be null!");
			TOut[] res=Array.Empty<TOut>();
			foreach(TIn sel in source)
			{
				Array.Resize(ref res, res.Length+1);
				res[^1]=predicate(sel);
			}
			return res;
		}
		/// <inheritdoc cref="Iterate{TIn, TOut}(IEnumerable{TIn}, Func{TIn, TOut})"/>
		public static async Task<TOut[]> IterateAsync<TIn, TOut>(this IEnumerable<TIn> source, Func<TIn, TOut> predicate)
		{
			if(source is null)
				return Array.Empty<TOut>();
			if(predicate is null)
				throw new Exception("Function cannot be null!");
			TOut[] res=Array.Empty<TOut>();
			int i=0;
			foreach(TIn sel in source)
			{
				if(i%100==0)
					await Task.Delay(1);
				Array.Resize(ref res, res.Length+1);
				res[^1]=predicate(sel);
				i++;
			}
			return res;
		}

	}
}