using System.Collections.Immutable;

namespace Immutable;

public class ImmutableStackTests
{
    [Fact]
    public void A_new_stack_is_empty()
    {
        var sut = new MyImmutableStack();
        Assert.True(sut.IsEmpty());
    }

    [Fact]
    public void A_stack_after_a_push_is_not_empty()
    {
        var sut = new MyImmutableStack();
        sut = sut.Push(10);
        Assert.False(sut.IsEmpty());
    }

    [Fact]
    public void Calling_top_on_an_new_list_throws_an_exception()
    {
        var sut = new MyImmutableStack();
        Assert.Throws<Exception>(() => sut.Top());
    }

    // [Fact]
    // public void Calling_top_on_an_empty_list_throws_an_exception()
    // {
    //     var sut = new MyImmutableStack();
    //     sut.Push(10);
    //     sut.Pop();
    //     Assert.Throws<Exception>(() => sut.Top());
    // }

    [Fact]
    public void After_a_push_the_pushed_value_is_on_top()
    {
        var sut = new MyImmutableStack();
        sut = sut.Push(10);
        Assert.Equal(10, sut.Top());
    }

    [Fact]
    public void After_two_pushes_and_a_pop_the_first_value_is_on_top()
    {
        var sut = new MyImmutableStack();
        sut = sut.Push(10);
        sut = sut.Push(5);
        sut = sut.Pop();
        Assert.Equal(10, sut.Top());
    }

    [Fact]
    public void After_two_pushes_and_a_pop_all_version_of_the_stack_still_exist()
    {
        var emptySut = new MyImmutableStack();
        var with10 = emptySut.Push(10);
        var with5and10 = with10.Push(5);
        var afterPop = with5and10.Pop();
        Assert.True(emptySut.IsEmpty());
        Assert.Equal(10, with10.Top());
        Assert.Equal(5, with5and10.Top());
        Assert.Equal(10, afterPop.Top());
        Assert.True(Object.ReferenceEquals(with10, afterPop));
    }
}

public class MyImmutableStack
{
    public class Node(int value, MyImmutableStack rest)
    {
        public readonly int Value = value;
        public readonly MyImmutableStack? Rest = rest;

    }

    private readonly Node? head = null;

    public MyImmutableStack()
    {
    }

    private MyImmutableStack(int topValue, MyImmutableStack rest)
    {
        head = new Node (topValue, rest);
    }

    public bool IsEmpty()
    {
        return head is null;
    }

    public MyImmutableStack Push(int value)
    {
        return new MyImmutableStack(value, this);
    }

    public int Top()
    {
        if (head is null) throw new Exception("Cannot look at the top of an empty list.");
        return head.Value;
    }

    public MyImmutableStack Pop()
    {
        if (head is null) throw new Exception("Cannot pop from an empty list.");
        return head.Rest;

    }
}