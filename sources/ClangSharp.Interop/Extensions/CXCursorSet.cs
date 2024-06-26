// Copyright (c) .NET Foundation and Contributors. All Rights Reserved. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using System;

namespace ClangSharp.Interop;

public unsafe partial struct CXCursorSet(IntPtr handle) : IDisposable, IEquatable<CXCursorSet>
{
    public IntPtr Handle { get; set; } = handle;

    public static implicit operator CXCursorSet(CXCursorSetImpl* value) => new CXCursorSet((IntPtr)value);

    public static implicit operator CXCursorSetImpl*(CXCursorSet value) => (CXCursorSetImpl*)value.Handle;

    public static bool operator ==(CXCursorSet left, CXCursorSet right) => left.Handle == right.Handle;

    public static bool operator !=(CXCursorSet left, CXCursorSet right) => left.Handle != right.Handle;

    public static CXCursorSet Create() => clang.createCXCursorSet();

    public readonly bool Contains(CXCursor cursor) => clang.CXCursorSet_contains(this, cursor) != 0;

    public void Dispose()
    {
        if (Handle != IntPtr.Zero)
        {
            clang.disposeCXCursorSet(this);
            Handle = IntPtr.Zero;
        }
    }

    public override readonly bool Equals(object? obj) => (obj is CXCursorSet other) && Equals(other);

    public readonly bool Equals(CXCursorSet other) => this == other;

    public override readonly int GetHashCode() => Handle.GetHashCode();

    public readonly bool Insert(CXCursor cursor) => clang.CXCursorSet_insert(this, cursor) != 0;
}
