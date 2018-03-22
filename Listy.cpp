#include<iostream>
using namespace std;

struct node
{
	int val;
	node*next;
};

void AddNode(node*&H, int x)
{
	node*p = new node;
	p->val = x;
	p->next = H;
	H = p;
}

void ShowList(node*H)
{
	cout << "H->";
	node* p = H;
	while (p != NULL)
	{
		cout << p->val << "->";
		p = p->next;
	}
	cout << "NULL" << endl;
}


void del(node*&H)
{
	if (H != NULL)
	{
		node*p = H;
		H = H->next;//H = H->next
		delete p;
	}
}

void AddToEnd(node*&H, int x)
{
	if (H == NULL)
	{
		AddNode(H, x);

	}
	else
	{
		node *p = H;

		while (p->next != NULL)
		{
			p = p->next;
		}
		AddNode(p->next, x);
	}
}


void copy(node*&H)
{
	node*p = H;
	while (p != NULL)
	{
		AddNode(p->next, p->val);
		p = p->next->next;
	}
}

void poww(node*&H)
{
	node *p = H;
	while (p != NULL)
	{
		for (int i = 1; i < p->val; i++)
		{
			AddNode(p->next, p->val);
			p = p->next;
		}

		p = p->next;
	}
}

void sum(node*&H) {
	node*p = H;
	while (p != NULL && p->next != NULL)
	{
		AddNode(p->next -> next, p->val + p->next->val);
		p = p->next->next->next;
	}
}

void Select(node*&H) {
	node*p = H;
	while (p != NULL && p->next != NULL)
	{
		del(p->next);
		p = p->next;
	}
}

void Chang(node*H) {
	if (p != Null && p->next != NULL)
	{
		node *P = H
			H = p->next;
		p->next = H
			H->next = p;
	}
}
int main()
{
	node* H = NULL;
	AddToEnd(H, 1);
	AddToEnd(H, 2);
	AddToEnd(H, 3);
	AddToEnd(H, 4);
	AddToEnd(H, 5);
	ShowList(H);
	Select(H);
	ShowList(H);
	system("PAUSE");
	return 0;
}