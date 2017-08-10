////////////////////////////////////////////////////////////
// Single-linked list template class in STL-like manner
// 
// Copyright (c) 2005, Alexey Provotorov.
//
// slist.h : header file
////////////////////////////////////////////////////////////

#pragma once
#ifndef _SLIST_
#define _SLIST_

#include <stdexcept>

#define _GENERIC_BASE	_Node

using namespace std;

namespace apstd
{

///
/// base for forward iterators
///
template<class _Ty,	class _Diff, class _Pointer, class _Reference>
struct _Fwdit : 
	public iterator<forward_iterator_tag, _Ty, _Diff,
					_Pointer, _Reference>
{	
};

///
/// base class for _SLNode to hold allocator _Alnod
///
template<class _Ty, class _Alloc>
class _SLNode
{	
protected:
	struct _Node;
	friend struct _Node;
	typedef _POINTER_X(_GENERIC_BASE, _Alloc) _Genptr;

	///
	/// slist node
	///
	struct _Node
	{	
		///
		/// construct a node with value
		///
		_Node(_Genptr _Nextarg, const _Ty& _Myvalarg)
			: _Next(_Nextarg), _m_val(_Myvalarg)
		{	
		}

		// successor node, or first element if head
		_Genptr _Next;	
		// the stored value, unused if head
		_Ty _m_val;	
	};

	// construct allocator from _Al
	_SLNode(_Alloc _Al)
		: _Alnod(_Al)
	{	
	}

	// allocator object for nodes
	typename _Alloc::template rebind<_Node>::other _Alnod;	
};

///
/// base class for _SListBase to hold allocator _Alptr
///
template<class _Ty,	class _Alloc>
class _SLNodePtr :
	public _SLNode<_Ty, _Alloc>
{	
protected:
	typedef typename _SLNodePtr<_Ty, _Alloc>::_Node _Node;
	typedef _POINTER_X(_Node, _Alloc) _Nodeptr;

	// construct base, and allocator from _Al
	_SLNodePtr(_Alloc _Al)
		: _SLNode<_Ty, _Alloc>(_Al), _Alptr(_Al)
	{	
	}

	// allocator object for pointers to nodes
	typename _Alloc::template rebind<_Nodeptr>::other _Alptr;	
};

///
/// base class for slist to hold allocator _Alval
///
template<class _Ty,	class _Alloc>
class _SListBase :
	public _SLNodePtr<_Ty, _Alloc>
{	
protected:
	typedef typename _Alloc::template rebind<_Ty>::other _Alty;

	///
	/// construct base, and allocator from _Al
	///
	_SListBase(_Alloc _Al = _Alloc()) :
		_SLNodePtr<_Ty, _Alloc>(_Al), _Alval(_Al)
	{	
	}

	// allocator object for values stored in nodes
	_Alty _Alval;	
};

///
/// single-linked list
///
template<class _Ty,	class _Ax = allocator<_Ty> >
class slist	:
	public _SListBase<_Ty, _Ax>
{	
public:
	typedef slist<_Ty, _Ax> _Myt;
	typedef _SListBase<_Ty, _Ax> _Mybase;
	typedef typename _Mybase::_Alty _Alloc;

protected:
	typedef typename _SLNode<_Ty, _Ax>::_Genptr _Genptr;
	typedef typename _SLNode<_Ty, _Ax>::_Node _Node;
	typedef _POINTER_X(_Node, _Alloc) _Nodeptr;
	typedef _REFERENCE_X(_Nodeptr, _Alloc) _Nodepref;
	typedef typename _Alloc::reference _Vref;

	///
	/// return reference to successor pointer in node
	///
	static _Nodepref _Nextnode(_Nodeptr _Pnode)
	{	
		return ((_Nodepref) (*_Pnode)._Next);
	}

	///
	/// return reference to value in node
	///
	static _Vref _Myval(_Nodeptr _Pnode)
	{	
		return ((_Vref) (*_Pnode)._m_val);
	}

public:
	typedef _Alloc allocator_type;
	typedef typename _Alloc::size_type size_type;
	typedef typename _Alloc::difference_type _Dift;
	typedef _Dift difference_type;
	typedef typename _Alloc::pointer _Tptr;
	typedef typename _Alloc::const_pointer _Ctptr;
	typedef _Tptr pointer;
	typedef _Ctptr const_pointer;
	typedef typename _Alloc::reference _Reft;
	typedef _Reft reference;
	typedef typename _Alloc::const_reference const_reference;
	typedef typename _Alloc::value_type value_type;

	// friends & forward declarations
	class const_iterator;
	friend class const_iterator;
	class iterator;
	friend class iterator;

	///
	/// iterator for nonmutable slist
	///
	class const_iterator :
		public _Fwdit<_Ty, _Dift, _Ctptr, const_reference>
	{	
	public:
		typedef forward_iterator_tag iterator_category;
		typedef _Ty value_type;
		typedef _Dift difference_type;
		typedef _Ctptr pointer;
		typedef const_reference reference;

		// construct with null node pointer
		const_iterator() : 
			_Ptr(0)
		{	
		}

		// construct with node pointer _Pnode
		const_iterator(_Nodeptr _Pnode) :
			_Ptr(_Pnode)
		{	
		}

		// return designated value
		const_reference operator*() const
		{	
			return (_Myval(_Ptr));
		}

		// return pointer to class object
		_Ctptr operator->() const
		{	
			return (&**this);
		}

		// preincrement
		const_iterator& operator++()
		{	
			_Ptr = _Nextnode(_Ptr);
			return (*this);
		}

		// postincrement
		const_iterator operator++(int)
		{	
			const_iterator _Tmp = *this;
			++*this;
			return (_Tmp);
		}

		// test for iterator equality
		bool operator==(const const_iterator& _Right) const
		{	
			return (_Ptr == _Right._Ptr);
		}

		// test for iterator inequality
		bool operator!=(const const_iterator& _Right) const
		{	
			return (!(*this == _Right));
		}

		// return node pointer
		_Nodeptr _Mynode() const
		{	
			return (_Ptr);
		}

	protected:
		// pointer to node
		_Nodeptr _Ptr;	
	};

	///
	/// iterator for mutable slist
	///
	class iterator :
		public const_iterator
	{	
	public:
		typedef forward_iterator_tag iterator_category;
		typedef _Ty value_type;
		typedef _Dift difference_type;
		typedef _Tptr pointer;
		typedef _Reft reference;

		// construct with null node
		iterator()
		{
		}

		// construct with node pointer _Pnode
		iterator(_Nodeptr _Pnode) :
			const_iterator(_Pnode)
		{
		}

		// return designated value
		reference operator*() const
		{	
			return ((reference)** (const_iterator*) this);
		}

		// return pointer to class object
		_Tptr operator->() const
		{	
			return (&**this);
		}

		// preincrement
		iterator& operator++()
		{	
			++(* (const_iterator*) this);
			return (*this);
		}

		// postincrement
		iterator operator++(int)
		{	
			iterator _Tmp = *this;
			++*this;
			return (_Tmp);
		}
	};

// Construction/Destruction

	// construct empty slist
	slist()	:
		_Mybase(), _m_Last(_NewNode()), _m_size(0)
	{	
	}

	// construct empty slist, allocator
	explicit slist(const _Alloc& _Al) :
		_Mybase(_Al), _m_Last(_NewNode()), _m_size(0)
	{	
	}

	// destroy the object
	~slist()
	{	
		_CleanUp();
	}

// Attributes

	// return iterator for beginning of mutable sequence
	iterator begin()
	{	
		return (iterator(_Nextnode(_Nextnode(_m_Last))));
	}

	// return iterator for beginning of non-mutable sequence
	const_iterator begin() const
	{	
		return (const_iterator(_Nextnode(_Nextnode(_m_Last))));
	}

	// return iterator for end of mutable sequence
	iterator end()
	{
		return (iterator(_Nextnode(_m_Last)));
	}

	// return iterator for end of nonmutable sequence
	const_iterator end() const
	{
		return (const_iterator(_Nextnode(_m_Last)));
	}

	// return length of sequence
	size_type size() const
	{
		return (_m_size);
	}

	// return maximum possible length of sequence
	size_type max_size() const
	{	
		return (this->_Alval.max_size());
	}

	// test if sequence is empty
	bool empty() const
	{	
		return (0 == m_size);
	}

	// return allocator object for values
	allocator_type get_allocator() const
	{
		return (this->_Alval);
	}

	// return first element of mutable sequence
	reference front()
	{	
		return (*begin());
	}

	// return first element of nonmutable sequence
	const_reference front() const
	{	
		return (*begin());
	}

	// return last element of mutable sequence
	reference back()
	{	
		return _Myval(_m_Last);
	}

	// return last element of nonmutable sequence
	const_reference back() const
	{
		return _Myval(_m_Last);
	}

	// insert element at beginning
	void push_front(const _Ty& _Val)
	{	
		_Insert(end(), _Val);
	}

// Operations

	// erase element at beginning
	void pop_front()
	{	
		erase(begin());
	}

	// insert element at end
	void push_back(const _Ty& _Val)
	{	
		_Insert(iterator(_m_Last), _Val);
		_m_Last = _Nextnode(_m_Last);
	}

	// erase element at end
	void pop_back()
	{	
		erase(*iterator(_m_Last));
	}

	// insert _Val just after _Prev
	iterator insert(iterator _Prev, const _Ty& _Val)
	{	
		_Insert(_Prev, _Val);
		return (++_Prev);
	}

	// erase element at _Where
	iterator erase(iterator _Where)
	{
		iterator _Prev = _FindPrev(_Where);
		_Nodeptr _Pnode = (_Where++)._Mynode();

		if (_Pnode != _Nextnode(_m_Last)) // not slist head, safe to erase
		{	
			_Nextnode(_Prev._Mynode()) = _Nextnode(_Pnode);

			this->_Alnod.destroy(_Pnode);
			this->_Alnod.deallocate(_Pnode, 1);
			--_m_size;
		}

		return (_Where);
	}

	// erase [_First, _Last)
	iterator erase(iterator _First, iterator _Last)
	{
		if (beign() == _First && end() == _Last)
		{
			clear();
		}
		else
		{
			while (_First != _Last)
			{
				_First = erase(_First);
			}
		}
		return (_Last);
	}

	// erase all
	void clear()
	{	
		_Nodeptr _Pnext;
		_m_Last = _Nextnode(_m_Last);
		_Nodeptr _Pnode = _Nextnode(_m_Last);
		_Nextnode(_m_Last) = _m_Last;
		_m_size = 0;

		// delete an element
		for (; _Pnode != _m_Last; _Pnode = _Pnext)
		{	
			_Pnext = _Nextnode(_Pnode);
			this->_Alnod.destroy(_Pnode);
			this->_Alnod.deallocate(_Pnode, 1);
		}
	}

// Implementation
protected:

	// allocate a head node and set links
	_Nodeptr _NewNode()
	{	
		_Nodeptr _Pnode = this->_Alnod.allocate(1);
		int _Linkcnt = 0;

		_TRY_BEGIN
			this->_Alptr.construct(&_Nextnode(_Pnode), _Pnode);
			++_Linkcnt;
		_CATCH_ALL
			if (0 < _Linkcnt)
			{
				this->_Alptr.destroy(&_Nextnode(_Pnode));
			}
			this->_Alnod.deallocate(_Pnode, 1);
		_RERAISE;
		_CATCH_END
		return (_Pnode);
	}

	// allocate a node and set links and value
	_Nodeptr _NewNode(_Nodeptr _Next, const _Ty& _Val)
	{	
		_Nodeptr _Pnode = this->_Alnod.allocate(1);
		_TRY_BEGIN
			new ((void *)_Pnode) _Node(_Next, _Val);
		_CATCH_ALL
			this->_Alnod.deallocate(_Pnode, 1);
		_RERAISE;
		_CATCH_END
		return (_Pnode);
	}

	// insert _Val just after _Prev
	void _Insert(iterator _Prev, const _Ty& _Val)
	{	
		_Nodeptr _Pnode = _Prev._Mynode();
		_Nodeptr _Newnode = _NewNode(_Nextnode(_Pnode), _Val);

		if (_Nextnode(_Pnode) == _Pnode)
		{
			//it was the last item
			//now it changed for new node
			_m_Last = _Newnode;
		}
		
		_Incsize(1);
		_Nextnode(_Pnode) = _Newnode;
	}

	// free all storage
	void _CleanUp()
	{	
		clear();
		this->_Alptr.destroy(&_m_Last);
		this->_Alnod.deallocate(_m_Last, 1);
		_m_Last = 0;
	}

	// alter element count, with checking
	void _Incsize(size_type _Count)
	{	
		if (max_size() - _m_size < _Count)
		{
			_THROW(length_error, "slist<T> too long");
		}
		_m_size += _Count;
	}

// Data Members
private:
	// pointer to last node (before end)
	_Nodeptr _m_Last;

	// number of elements
	size_type _m_size;	
};

// test for slist equality
template<class _Ty, class _Alloc> inline
bool operator==(const slist<_Ty, _Alloc>& _Left,
				const slist<_Ty, _Alloc>& _Right)
{
	return (_Left.size() == _Right.size()
		&& equal(_Left.begin(), _Left.end(), _Right.begin()));
}

// test for slist inequality
template<class _Ty, class _Alloc> inline
bool operator!=(const slist<_Ty, _Alloc>& _Left,
				const slist<_Ty, _Alloc>& _Right)
{	
	return (!(_Left == _Right));
}

}; // namespace apstd

#endif /* _SLIST_ */
