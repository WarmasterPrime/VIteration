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
		/// <exception cref="ArgumentNullException">Predicate function null exception.</exception>
		public static TOut[] Iterate<TIn, TOut>(this IEnumerable<TIn> source, Func<TIn, TOut> predicate)
		{
			if(source is null)
				return Array.Empty<TOut>();
			if(predicate is null)
				throw new ArgumentNullException(nameof(predicate), "The predicate function cannot be null!");
			TOut[] res=Array.Empty<TOut>();
			foreach(TIn sel in source)
			{
				Array.Resize(ref res, res.Length+1);
				res[^1]=predicate(sel);
			}
			return res;
		}
		/// <inheritdoc cref="Iterate{TIn, TOut}(IEnumerable{TIn}, Func{TIn, TOut})"/>
		public static TOut[] IterateMultiThread<TIn, TOut>(this IEnumerable<TIn> source, Func<TIn, TOut> predicate)
		{
			if(source is null)
				return Array.Empty<TOut>();
			if(predicate is null)
				throw new ArgumentNullException(nameof(predicate), "The predicate function cannot be null!");
			SemaphoreSlim semaphore=new(Environment.ProcessorCount);
			IEnumerable<TOut> RunPredicate(TIn item)
			{
				semaphore.Wait();
				try
				{
					return new[] { predicate(item) };
				}
				finally
				{
					semaphore.Release();
				}
			}
			return source.AsParallel().Select(RunPredicate).SelectMany(q=>q).AsEnumerable().ToArray();
		}
		/// <inheritdoc cref="Iterate{TIn, TOut}(IEnumerable{TIn}, Func{TIn, TOut})"/>
		public static async Task<TOut[]> IterateAsync<TIn, TOut>(this IEnumerable<TIn> source, Func<TIn, Task<TOut>> predicate)
		{
			if(source is null)
				return Array.Empty<TOut>();
			if(predicate is null)
				throw new ArgumentNullException(nameof(predicate), "The predicate function cannot be null!");
			SemaphoreSlim semaphore=new(Environment.ProcessorCount);
			async Task<TOut> RunPredicate(TIn item)
			{
				await semaphore.WaitAsync();
				try
				{
					return await predicate(item);
				}
				finally
				{
					semaphore.Release();
				}
			}
			return await Task.WhenAll(source.Select(RunPredicate));
		}
		/// <inheritdoc cref="Iterate{TIn, TOut}(IEnumerable{TIn}, Func{TIn, TOut})"/>
		public static async Task<TOut[]> IterateAsync<TIn, TOut>(this IEnumerable<TIn> source, Func<TIn, TOut> predicate)
		{
			if(source is null)
				return Array.Empty<TOut>();
			if(predicate is null)
				throw new ArgumentNullException(nameof(predicate), "The predicate function cannot be null!");
			SemaphoreSlim semaphore=new(Environment.ProcessorCount);
			async Task<TOut> RunPredicate(TIn item)
			{
				await semaphore.WaitAsync();
				try
				{
					return predicate(item);
				}
				finally
				{
					semaphore.Release();
				}
			}
			return await Task.WhenAll(source.Select(RunPredicate));
		}

	}
}