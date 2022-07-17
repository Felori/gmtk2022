using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
	public static T Instance { get; private set; }

	public virtual void Awake()
	{
		if (!Application.isPlaying) return;

		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		if (Instance == null)
			Instance = this as T;
	}

	protected virtual void OnDestroy()
	{
		if (ReferenceEquals(Instance, this))
			Instance = null;
	}
}