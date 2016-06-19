using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BlueprintBoard
{
    /// <summary>
    /// Provides mechanisms for undo/redo functionality.
    /// </summary>
    class RedoUndoManager<T>
    {
        private readonly Stack<T> undoStack = new Stack<T>();
        private readonly Stack<T> redoStack = new Stack<T>();
        private T currentState;
        private bool hasCurrentState; // So we don't push an empty state at first

        private bool canUndo;
        /// <summary>
        /// Gets a value indicating whether an undo can be performed.
        /// </summary>
        public bool CanUndo
        {
            get { return canUndo; }
            private set
            {
                if (canUndo != value)
                {
                    canUndo = value;
                    OnCanUndoChanged();
                }
            }
        }

        private bool canRedo;
        /// <summary>
        /// Gets a value indicating whether a redo can be performed.
        /// </summary>
        public bool CanRedo
        {
            get { return canRedo; }
            private set
            {
                if (canRedo != value)
                {
                    canRedo = value;
                    OnCanRedoChanged();
                }
            }
        }

        /// <summary>
        /// Occurs when the value of the <see cref="CanUndo"/> property has changed.
        /// </summary>
        [Description("Occurs when the value of the CanUndo property has changed.")]
        public event EventHandler CanUndoChanged;
        /// <summary>
        /// Raises the <see cref="CanUndoChanged"/> event.
        /// </summary>
        protected virtual void OnCanUndoChanged() => CanUndoChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Occurs when the value of the <see cref="CanRedo"/> property has changed.
        /// </summary>
        [Description("Occurs when the value of the CanRedo property has changed")]
        public event EventHandler CanRedoChanged;
        /// <summary>
        /// Raises the <see cref="CanRedoChanged"/> event.
        /// </summary>
        protected virtual void OnCanRedoChanged() => CanRedoChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Adds a state to the manager.
        /// </summary>
        public void AddState(T state)
        {
            // Do this or states will get harder to keep track of and
            // will make less sense as states are added
            redoStack.Clear();

            if (hasCurrentState)
            {
                undoStack.Push(currentState);
            }

            currentState = state;
            hasCurrentState = true;
            UpdateCanDo();
        }

        /// <summary>
        /// Updates the can redo and undo properties.
        /// </summary>
        private void UpdateCanDo()
        {
            CanUndo = undoStack.Count > 0;
            CanRedo = redoStack.Count > 0;
        }

        /// <summary>
        /// Step backward in the added states.
        /// </summary>
        /// <returns>Returns the last state in the undo stack.</returns>
        public T Undo()
        {
            redoStack.Push(currentState);
            currentState = undoStack.Pop();
            UpdateCanDo();
            return currentState;
        }

        /// <summary>
        /// Steps forward in the added states.
        /// </summary>
        /// <returns>Returns the last state in the redo stack.</returns>
        public T Redo()
        {
            undoStack.Push(currentState);
            currentState = redoStack.Pop();
            UpdateCanDo();
            return currentState;
        }
    }
}