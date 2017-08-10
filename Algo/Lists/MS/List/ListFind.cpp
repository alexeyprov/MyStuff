////////////////////////////////////////////////////////////
// Implementation of single-linked list search routine
// Uses apstd::slist as container by default
// 
// Copyright (c) 2005, Alexey Provotorov.
//
// list.cpp : implementation file
////////////////////////////////////////////////////////////

#include <iostream>

using namespace std;

// list item type - unsigned integer
typedef unsigned int ItemType;

// ...to use single-linked list
// comment this out to use STL's bidirectional list
#define USE_SLIST

// ...to use STLPort's single-linked list (slist)
// comment this out to use proprietary implementation of slist
//#define STLPORT_SLIST

#ifdef USE_SLIST //Use single-linked list

#ifdef STLPORT_SLIST // STLPort single-linked list

#include <slist>
typedef slist<ItemType> ListType;

#else //!STLPORT_SLIST => AP single-linked list

#include "slist.h"
typedef apstd::slist<ItemType> ListType;

#endif //STLPORT_SLIST

#else //!USE_SLIST => use std::list, even though it is bidirectional

#include <list>
typedef std::list<ItemType> ListType;

#endif //USE_LIST

typedef ListType::const_iterator IListType;

// Helper macro - determines count of items in static array
#define _countof(ar) (sizeof(ar) / sizeof(ar[0]))

/// <summary>
/// Find n-th element from the end in the container specified by
/// two input iterators.
/// </summary>
/// <param name="_First">Input (forward) iterator - begin</param>
/// <param name="_Last">Input (forward) iterator - end (after last)</param>
/// <param name="n">Position of searched element from the last one
///	The position is 1-based.
///	</param>
template<class _InIt> inline
_InIt find_nth_from_end(_InIt _First, _InIt _Last, unsigned int n)
{
	// skip first n items
	_InIt p = _First;
	for (unsigned int i = 0; _First != _Last && i < n; ++_First, ++i)
	{
	}

	// if n-th item is too far, return _Last (not found)
	if (_First == _Last && i < n)
	{
		return _Last;
	}

	// iterate _First and p until _Last
	for (; _First != _Last; ++_First, ++p)
	{
	}

	return p;
}

/// <summary>
/// Runs a test case against find_nth_from_end() routine
/// List of integers is populated with numbers from sz down to 1
/// Hence, nth element from the end equals exactly n in case of valid input
/// </summary>
/// <param name="n">1-based position of searched element from end</param>
/// <param name="sz">Size of list</param>
/// <returns>true, if test case succeeded, false otherwise.</returns>
bool RunTestCase(unsigned int n, unsigned int sz)
{
	
	ListType l;
	static int nTestCase = 0;

	// print test case info
	cout << "================================" << endl;
	cout << "Test Case #" << ++nTestCase << endl;
	cout << "List Size = " << sz << "; Position from End = " << n << endl;

	// prepare list
	for (unsigned int i = 1; i <= sz; ++i)
	{
		l.push_front(i);
	}

	// find n-th from end
	IListType pEnd = l.end();
	IListType pElem = find_nth_from_end<IListType>(l.begin(), pEnd, n);
	
	bool status = false;

	// evaluate status
	// NB: not all C++ compilers guarantee short circuit evaluation
	// therefore we use if statement here
	if (pElem != pEnd)
	{
		status = (*pElem == n);
	}
	else
	{
		// in these cases the end() is the only possible result
		status = (0 == n || 0 == sz || n > sz);
	}

	// report status
	cout << (status ? "OK" : "Failed") << endl;
	return status;
}

/// <summary>
/// Runs test cases against searching routine.
/// All test cases use a pair of nonnegative numbers:
/// list size (sz) and position from end (n).
/// The test cases are the following
/// TC1: 0 = n = sz
/// TC2: 0 = n < sz
/// TC3: 0 < n < sz
/// TC4: 0 < n = sz
/// TC5: 0 < sz < n
/// TC6: 0 = sz < n
/// </summary>
/// <returns>true, if all test cases succeeded, false otherwise</returns>
bool TestList()
{
	// pairs of list test data
	// each pair specifies separate test case (n, sz)
	const unsigned int arTestData[][2] = {
		{0, 0}, // TC1
		{0, 5}, // TC2
		{5, 7}, // ...
		{5, 5},
		{5, 4},
		{5, 0}  // TC6
	};

	bool status = true;

	// run all test cases
	for (int i = 0, cnt = _countof(arTestData); i < cnt; ++i)
	{
		status &= RunTestCase(arTestData[i][0], arTestData[i][1]);
	}

	return status;
}

/// <summary>
/// Application entry point.
/// </summary>
/// <param name="argc">Count of command-line arguments</param>
/// <param name="argv">Values of command-line arguments</param>
/// <returns>0, if all test cases succeeded. -1 otherwise</returns>
int main(int argc, char* argv[])
{
	return (TestList() ? 0 : -1);
}