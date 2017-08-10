interface IWtsTarget
{
	void Foo();
	void Goo();
};

template <class TBase>
class CLogicalTarget :
	public IWtsTarget,
	public TBase
{
public:
	void Foo()
	{
	}

protected:
	void InternalGoo()
	{
	}
};


class CBaseDecorator
{
protected:
	int _sharData;
};


class CFakeDecorator :
	public CBaseDecorator
{
public:
	void Goo()
	{
		T* pThis = static_cast<T*>(this);
		pThis->InternalGoo();
	}
};

template <class T>
class CAltDecorator :
	public CBaseDecorator<T>
{
public:
	void Goo()
	{
		_sharData++;
	}
};

template<class T>
void TestDecorators()
{
	CLogicalTarget<CFakeDecorator> 
	IWtsTarget* dec
}