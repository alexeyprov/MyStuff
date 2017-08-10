// MbsUnpacker.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef unsigned short word;
typedef unsigned char byte;

void main(int argc, char* argv[])
{
	FILE*	h;
	word	n, blocks_total;
	word	ptr, next_ptr, tw1, tw2;
	byte	buf[0x200];
	int		i, j, k;
	int		block;
	word*	tbl400;
	int	tbl400_size;

	if (argv[1]==NULL)
	{
		printf ("use .msb file name as first argument\n");
		return;
	};
	h=fopen (argv[1], "rb");

	if (h==NULL)
	{
		printf ("cannot open this file\n");
		return;
	};

	if (fseek (h, 0x44, 0)!=0)
	{
		printf ("something wrong\n"); return;
	};

	if (fread (&blocks_total, 2, 1, h)!=1)
	{
		printf ("something wrong\n"); return;
	};	

	//printf ("word at 0x44(blocks_total)=0x%04X\n", blocks_total);

	if (fseek (h, 0x400, SEEK_SET)!=0)
	{
		printf ("something wrong"); return;
	};

	tbl400_size=blocks_total*2;

	if ((tbl400_size&0x1FF) != 0)
	{
		tbl400_size=tbl400_size + (0x200 - (tbl400_size&0x1FF));
	};

	//printf ("tbl400_size=0x%x\n", tbl400_size);

	// read tbl400

	tbl400=(word*)malloc (tbl400_size);
	
	if (fread (tbl400, tbl400_size, 1, h)!=1)
	{
		printf ("something wrong\n"); return;
	};
/*
	for (block=0; block<blocks_total; block++)
	{
		printf ("tbl400[%d]=%d\n", block, tbl400[block]);
	};
*/
	free (tbl400);

	for (block=0; block<blocks_total; block++)
	{
		//printf ("* block %d\n", block);
		if (fread (buf, 0x200, 1, h)!=1)
		{
			printf ("something wrong\n"); return;
		};

		n=(buf[0] | buf[1]<<8); // number of messages in block

		for (i=0;i<n;i++)
		{
			j=i*6;

			tw1=(buf[j+2] | buf[j+3]<<8);
			tw2=(buf[j+4] | buf[j+5]<<8);
			ptr=(buf[j+6] | buf[j+7]<<8);
			next_ptr=(buf[j+12] | buf[j+13]<<8);

			printf ("%05d, %05d, \"", tw1, tw2);

			for (k=ptr; k<next_ptr; k++)
			{
				if (buf[k]==0x0A)
					printf ("\\n");
				else
					printf ("%c", buf[k]);
			};
			printf ("\"\n");
		};
	};
	fclose (h);

}

