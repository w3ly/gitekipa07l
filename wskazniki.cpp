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
	while(p != NULL)
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
int main()
{
	node* H = NULL;
	AddNode(H, 4);
	AddNode(H, 3);
	AddNode(H, 23);
	AddNode(H, 45);
	//del(H);
	AddNode(H->next, 10);
	ShowList(H);
	system("PAUSE");
	return 0;
}