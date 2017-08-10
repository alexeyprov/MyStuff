// AKN.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

struct FixedCategory
{
	TCHAR text[128];
	int id;

	FixedCategory(int id, LPCTSTR lpczText)
                //: id(id)
    {
			this->id = id;
            _tcscpy(text, lpczText);
    }

        FixedCategory(const FixedCategory &var)
        {
                *this = var;
        }

        FixedCategory& operator=(const FixedCategory &var)
        {
                if (this != &var)
                {
                        id = var.id;
                        _tcscpy(text, var.text);
                }

                return *this;
        }
};

struct FixedVariable
{
        TCHAR text[128];
        int id;
        std::vector< FixedCategory > aCats;

        FixedVariable(int id = 0, LPCTSTR lpczText = 0)
                : id(id)
        {
                _tcscpy(text, lpczText);
        }

        ~FixedVariable()
        {
			aCats.clear();
        }

        FixedVariable(const FixedVariable &var)
        {
                *this = var;
        }

        FixedVariable& operator=(const FixedVariable &var)
        {
                if (this != &var)
                {
                        id = var.id;
                        _tcscpy(text, var.text);

                        aCats.clear();
//                      std::copy(var.aCats.begin(), var.aCats.end(), std::back_inserter(aCats));

                        // there is to replace std::copy
                        std::vector< FixedCategory >::const_iterator it = NULL;

                        aCats.reserve(var.aCats.size());
                        for ( it = var.aCats.begin(); it != var.aCats.end(); it++ )
                        {
                                aCats.push_back(*it);
                        }
                }

                return *this;
        }
};

class FixedVariables
{
public:
        std::vector< FixedVariable > m_vars;

       // .....
        FixedVariables()
        {
        }

        FixedVariable& operator[] (int index)
        {
                return m_vars[index];
        }
        // .....
};


int _tmain(int argc, _TCHAR* argv[])
{
	FixedVariables vars;
	FixedVariable v(10, _T("letters")), q(20, _T("Big letters"));
	FixedCategory c(1, _T("alpha")), d(2, _T("beta")), e(3, _T("gamma"));

	v.aCats.push_back(c);
	v.aCats.push_back(e);

	q.aCats.push_back(c);
	q.aCats.push_back(d);
	q.aCats.push_back(e);

	vars.m_vars.push_back(v);
	vars.m_vars.push_back(q);
	return 0;
}

