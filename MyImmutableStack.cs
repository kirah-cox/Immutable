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

    [Fact]
    public void Calling_top_on_an_empty_list_throws_an_exception()
    {
        var sut = new MyImmutableStack();
        sut = sut.Push(10);
        sut = sut.Pop();
        Assert.Throws<Exception>(() => sut.Top());
    }

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

    [Fact]
    public void A_new_deque_is_empty()
    {
        var sut = new MyImmutableDeque();
        Assert.True(sut.IsEmpty());
    }

    [Fact]
    public void A_deque_after_a_push_to_front_is_not_empty()
    {
        var sut = new MyImmutableDeque();
        sut = sut.PushFront(10);
        Assert.False(sut.IsEmpty());
    }

    [Fact]
    public void A_deque_after_a_push_to_back_is_not_empty()
    {
        var sut = new MyImmutableDeque();
        sut = sut.PushBack(10);
        Assert.False(sut.IsEmpty());
    }

    [Fact]
    public void After_a_push_to_front_the_pushed_value_is_on_top()
    {
        var sut = new MyImmutableDeque();
        sut = sut.PushFront(10);
        Assert.Equal(10, sut.Front());
    }

    [Fact]
    public void After_a_push_to_back_the_pushed_value_is_at_back()
    {
        var sut = new MyImmutableDeque();
        sut = sut.PushBack(10);
        Assert.Equal(10, sut.Back());
    }

    [Fact]
    public void Calling_front_on_an_new_list_throws_an_exception()
    {
        var sut = new MyImmutableDeque();
        Assert.Throws<Exception>(() => sut.Front());
    }

    [Fact]
    public void Calling_front_on_an_empty_list_throws_an_exception()
    {
        var sut = new MyImmutableDeque();
        sut = sut.PushFront(10);
        sut = sut.PopFront();
        Assert.Throws<Exception>(() => sut.Front());
    }

    [Fact]
    public void Calling_back_on_an_new_list_throws_an_exception()
    {
        var sut = new MyImmutableDeque();
        Assert.Throws<Exception>(() => sut.Back());
    }

    [Fact]
    public void Calling_back_on_an_empty_list_throws_an_exception()
    {
        var sut = new MyImmutableDeque();
        sut = sut.PushBack(10);
        sut = sut.PopBack();
        Assert.Throws<Exception>(() => sut.Back());
    }

    [Fact]
    public void After_two_pushes_and_a_pop_on_the_front_the_first_value_is_on_top()
    {
        var sut = new MyImmutableDeque();
        sut = sut.PushFront(10);
        sut = sut.PushFront(5);
        sut = sut.PopFront();
        Assert.Equal(10, sut.Front());
    }

    [Fact]
    public void After_two_pushes_and_a_pop_on_the_back_the_first_value_is_at_the_back()
    {
        var sut = new MyImmutableDeque();
        sut = sut.PushBack(10);
        sut = sut.PushBack(5);
        sut = sut.PopBack();
        Assert.Equal(10, sut.Back());
    }

    [Fact]
    public void Rebalance_works_on_back()
    {
        var sut = new MyImmutableDeque();
        sut = sut.PushBack(10);
        sut = sut.PushBack(5);
        sut = sut.PushBack(7);
        sut = sut.PushBack(3);
        sut = sut.Rebalance();
        Assert.Equal(3, sut.Back());
        Assert.Equal(10, sut.Front());
    }

    [Fact]
    public void Rebalance_works_on_front()
    {
        var sut = new MyImmutableDeque();
        sut = sut.PushFront(10);
        sut = sut.PushFront(5);
        sut = sut.PushFront(7);
        sut = sut.PushFront(3);
        sut = sut.Rebalance();
        Assert.Equal(10, sut.Front());
        Assert.Equal(3, sut.Back());
    }
}

public class MyImmutableDeque
{
    private MyImmutableStack front;
    private MyImmutableStack back;
    public int Count { get; private init; }

    public MyImmutableDeque() : this(new MyImmutableStack(), new MyImmutableStack()) {

    }

    public MyImmutableDeque(MyImmutableStack newFront, MyImmutableStack newBack)
    {
        front = newFront;
        back = newBack;
        Count = newFront.Count + newBack.Count;
    }

    public bool IsEmpty()
    {
        return Count == 0;
    }

    public MyImmutableDeque Rebalance()
    {
        var tempStack = new MyImmutableStack();
        var backwardTemp = new MyImmutableStack();
        bool topNotNull = true;
        int tempStackLength = 0;

        if (front.Count < 1 && back.Count > 1)
        {
            while (topNotNull)
            {
                try
                {
                    tempStack = tempStack.Push(back.Top());
                    back.Pop();
                    tempStackLength++;
                }
                catch
                {
                    topNotNull = false;
                }
            }

            for (int i = 0; i <= (tempStackLength / 2); i++)
            {
                backwardTemp = backwardTemp.Push(tempStack.Top());
                tempStack.Pop();
            }

            for (int i = 0; i <= (tempStackLength / 2); i++)
            {
                front = front.Push(backwardTemp.Top());
                backwardTemp.Top();
            }

            for (int i = 0; i <= (tempStackLength / 2); i++)
            {
                back = back.Push(tempStack.Top());
                tempStack.Pop();
            }
        }
        else if(back.Count < 1 && front.Count > 1)
        {
            while (topNotNull)
            {
                try
                {
                    tempStack = tempStack.Push(front.Top());
                    front.Pop();
                    tempStackLength++;
                }
                catch
                {
                    topNotNull = false;
                }
            }

            for (int i = 0; i <= (tempStackLength / 2); i++)
            {
                backwardTemp = backwardTemp.Push(tempStack.Top());
                tempStack.Pop();
            }

            for (int i = 0; i <= (tempStackLength / 2); i++)
            {
                back = back.Push(backwardTemp.Top());
                backwardTemp.Top();
            }

            for (int i = 0; i <= (tempStackLength / 2); i++)
            {
                front = front.Push(tempStack.Top());
                tempStack.Pop();
            }
        }
        return new MyImmutableDeque();
    }

    public MyImmutableDeque PushFront(int value)
    {
        return new MyImmutableDeque(front.Push(value), back);
    }

    public MyImmutableDeque PopFront()
    {
        Rebalance();
        return new MyImmutableDeque(front.Pop(), back);
    }

    public int Front()
    {
        return front.Top();
    }

    public MyImmutableDeque PushBack(int value)
    {
        return new MyImmutableDeque(front, back.Push(value));
    }

    public MyImmutableDeque PopBack()
    {
        return new MyImmutableDeque(front, back.Pop());
    }
    
    public int Back()
    {
        Rebalance();
        return back.Top();
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
        head = new Node(topValue, rest);
        Count = rest.Count + 1;
    }

    public bool IsEmpty()
    {
        return head is null;
    }

    public int Count { get; private init; } = 0;

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