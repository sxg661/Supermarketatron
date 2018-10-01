using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Optional<T>
{
    private T contents;
    private bool isEmpty;

    /// <summary>
    /// Reassigns the contents of the Optional
    /// </summary>
    /// <param name="contents"></param>
    public void setContents(T contents)
    {
        this.contents = contents;
        isEmpty = false;
    }

    /// <summary>
    /// Clears the contents of the optional making it empty
    /// </summary>
    public void clear()
    {
        isEmpty = true;
    }

    /// <summary>
    /// Checks if this contents has any contents
    /// </summary>
    /// <returns>True if there is contents</returns>
    public bool isPresent()
    {
        return !isEmpty;
    }

    /// <summary>
    /// Gets the contents of the Optional
    /// </summary>
    /// <returns>The contents</returns>
    public T get()
    {
        return contents;
    }

    /// <summary>
    /// Creates an empty Optional instance
    /// </summary>
    /// <returns></returns>
    public static Optional<T> empty()
    {
        Optional<T> give = new Optional<T>();
        give.clear();
        return give;
    }

    /// <summary>
    /// Creates an Optional with given contents
    /// </summary>
    /// <param name="contents"></param>
    /// <returns></returns>
    public static Optional<T> of(T contents)
    {
        Optional<T> give = new Optional<T>();
        give.setContents(contents);
        return give;
    }
}
