public interface IStatus<T> {

	T Current { get; }
	T Max { get; }

	void FullRestore();
	void Clear();
	void Restore(T point);
	void Remove(T point);
}
