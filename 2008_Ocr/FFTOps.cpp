#include "StdAfx.h"
#include ".\fftops.h"
#include <math.h>

#include <string>
#ifndef max
#define max(a,b)            (((a) > (b)) ? (a) : (b))
#endif

#ifndef min
#define min(a,b)            (((a) < (b)) ? (a) : (b))
#endif

BYTE CalcConfidenceFromFFT( double idx )
{
    if( idx < 0.00007 )
        return 100;
    if( idx < 0.00010 )
        return 99;
    if( idx < 0.00011 )
        return 98;
    if( idx < 0.00012 )
        return 97;
    if( idx < 0.00013 )
        return 96;
    if( idx < 0.00017 )
        return 95;
    if( idx < 0.00019 )
        return 94;
    if( idx < 0.00021 )
        return 93;
    if( idx < 0.00023 )
        return 92;
    if( idx < 0.00025 )
        return 91;
    if( idx < 0.00028 )
        return 90;
    if( idx < 0.00031 )
        return 89;
    if( idx < 0.00035 )
        return 88;
    if( idx < 0.00039 )
        return 87;
    if( idx < 0.00043 )
        return 86;
    if( idx < 0.00047 )
        return 85;
    if( idx < 0.00052 )
        return 84;
    if( idx < 0.00056 )
        return 83;
    if( idx < 0.00060 )
        return 82;
    if( idx < 0.00064 )
        return 81;
    if( idx < 0.00068 )
        return 80;
    if( idx < 0.00072 )
        return 79;
    if( idx < 0.00076 )
        return 78;
    if( idx < 0.00080 )
        return 77;
    if( idx < 0.00084 )
        return 76;
    if( idx < 0.00088 )
        return 75;
    if( idx < 0.00092 )
        return 74;
    if( idx < 0.00094 )
        return 73;
    if( idx < 0.00094 )
        return 72;
    if( idx < 0.001 )
        return 71;
    if( idx < 0.00106 )
        return 70;
    if( idx < 0.0011 )
        return 69;
    if( idx < 0.00115 )
        return 68;
    if( idx < 0.00120 )
        return 67;
    if( idx < 0.00125 )
        return 66;
    if( idx < 0.00130 )
        return 65;
    if( idx < 0.00135 )
        return 64;
    if( idx < 0.00140 )
        return 63;
    if( idx < 0.00145 )
        return 62;
    if( idx < 0.00150 )
        return 61;
    if( idx < 0.00155 )
        return 60;
    if( idx < 0.00160 )
        return 50;
    if( idx < 0.0017 )
        return 40;
    if( idx < 0.0018 )
        return 30;
    if( idx < 0.0019 )
        return 20;
    if( idx < 0.002 )
        return 10;
    return 0;
}

////////////////////////////////////////////////////////////////////////////////
/*
    Code modified from author below:
    Function FFT2->FFT2Sigs was modified and FFT, DFT and IsPowerof2 are used as they are
    The FFT2 function was modified so as to generate the FFT values that we want,
    we discard the image contents afterward
    Removed memory allocation functions in favor of static arrays,
    Since we only use it to process monochrome 1bpp bitmaps, we only use two values 1 or 0, this 
    appears to make the operation somewhat faster
  --------------------------------------------------------------------------------

	COPYRIGHT NOTICE, DISCLAIMER, and LICENSE:

	CxImage version 5.99c 17/Oct/2004

	CxImage : Copyright (C) 2001 - 2004, Davide Pizzolato

	Original CImage and CImageIterator implementation are:
	Copyright (C) 1995, Alejandro Aguilar Sierra (asierra(at)servidor(dot)unam(dot)mx)

	Covered code is provided under this license on an "as is" basis, without warranty
	of any kind, either expressed or implied, including, without limitation, warranties
	that the covered code is free of defects, merchantable, fit for a particular purpose
	or non-infringing. The entire risk as to the quality and performance of the covered
	code is with you. Should any covered code prove defective in any respect, you (not
	the initial developer or any other contributor) assume the cost of any necessary
	servicing, repair or correction. This disclaimer of warranty constitutes an essential
	part of this license. No use of any covered code is authorized hereunder except under
	this disclaimer.

	Permission is hereby granted to use, copy, modify, and distribute this
	source code, or portions hereof, for any purpose, including commercial applications,
	freely and without fee, subject to the following restrictions: 

	1. The origin of this software must not be misrepresented; you must not
	claim that you wrote the original software. If you use this software
	in a product, an acknowledgment in the product documentation would be
	appreciated but is not required.

	2. Altered source versions must be plainly marked as such, and must not be
	misrepresented as being the original software.

	3. This notice may not be removed or altered from any source distribution.

  --------------------------------------------------------------------------------

	Other information: about CxImage, and the latest version, can be found at the
	CxImage home page: http://www.xdp.it

  --------------------------------------------------------------------------------
*/

#ifndef PI
 #define PI 3.141592653589793f
#endif

////////////////////////////////////////////////////////////////////////////////
bool IsPowerof2(long x)
{
	long i=0;
	while ((1<<i)<x) i++;
	if (x==(1<<i)) return true;
	return false;
}
////////////////////////////////////////////////////////////////////////////////
/**
   This computes an in-place complex-to-complex FFT 
   x and y are the real and imaginary arrays of n=2^m points.
   o(n)=n*log2(n)
   dir =  1 gives forward transform
   dir = -1 gives reverse transform 
   Written by Paul Bourke, July 1998
   FFT algorithm by Cooley and Tukey, 1965 
*/
bool FFT(int dir,int m,double *x,double *y)
{
	long nn,i,i1,j,k,i2,l,l1,l2;
	double c1,c2,tx,ty,t1,t2,u1,u2,z;

	/* Calculate the number of points */
	nn = 1<<m;

	/* Do the bit reversal */
	i2 = nn >> 1;
	j = 0;
	for (i=0;i<nn-1;i++) {
		if (i < j) {
			tx = x[i];
			ty = y[i];
			x[i] = x[j];
			y[i] = y[j];
			x[j] = tx;
			y[j] = ty;
		}
		k = i2;
		while (k <= j) {
			j -= k;
			k >>= 1;
		}
		j += k;
	}

	/* Compute the FFT */
	c1 = -1.0;
	c2 = 0.0;
	l2 = 1;
	for (l=0;l<m;l++) {
		l1 = l2;
		l2 <<= 1;
		u1 = 1.0;
		u2 = 0.0;
		for (j=0;j<l1;j++) {
			for (i=j;i<nn;i+=l2) {
				i1 = i + l1;
				t1 = u1 * x[i1] - u2 * y[i1];
				t2 = u1 * y[i1] + u2 * x[i1];
				x[i1] = x[i] - t1;
				y[i1] = y[i] - t2;
				x[i] += t1;
				y[i] += t2;
			}
			z =  u1 * c1 - u2 * c2;
			u2 = u1 * c2 + u2 * c1;
			u1 = z;
		}
		c2 = sqrt((1.0 - c1) / 2.0);
		if (dir == 1)
			c2 = -c2;
		c1 = sqrt((1.0 + c1) / 2.0);
	}

	/* Scaling for forward transform */
	if (dir == 1) {
		for (i=0;i<nn;i++) {
			x[i] /= (double)nn;
			y[i] /= (double)nn;
		}
	}

   return true;
}
////////////////////////////////////////////////////////////////////////////////
/**
   Direct fourier transform o(n)=n^2
   Written by Paul Bourke, July 1998 
*/
bool DFT(int dir,long m,double *x1,double *y1,double *x2,double *y2)
{
   long i,k;
   double arg;
   double cosarg,sinarg;
   
   for (i=0;i<m;i++) {
      x2[i] = 0;
      y2[i] = 0;
      arg = - dir * 2.0 * PI * i / (double)m;
      for (k=0;k<m;k++) {
         cosarg = cos(k * arg);
         sinarg = sin(k * arg);
         x2[i] += (x1[k] * cosarg - y1[k] * sinarg);
         y2[i] += (x1[k] * sinarg + y1[k] * cosarg);
      }
   }
   
   /* Copy the data back */
   if (dir == 1) {
      for (i=0;i<m;i++) {
         x1[i] = x2[i] / m;
         y1[i] = y2[i] / m;
      }
   } else {
      for (i=0;i<m;i++) {
         x1[i] = x2[i];
         y1[i] = y2[i];
      }
   }
   
   return true;
}

/*
This function was modified from the ximage library with permission from the author,
For licensing information please see above. The author granted permission for any use 
including commercial applications.
*/
////////////////////////////////////////////////////////////////////////////////
/**
 * Computes the bidimensional FFT or DFT of the image.
 * - The images are processed as grayscale
 * - If the dimensions of the image are a power of, 2 the FFT is performed automatically.
 * - If dstReal and/or dstImag are NULL, the resulting images replaces the original(s).
 * - Note: with 8 bits there is a HUGE loss in the dynamics. The function tries
 *   to keep an acceptable SNR, but 8bit = 48dB...
 *
 * \param srcReal, srcImag: source images: One can be NULL, but not both
 * \param dstReal, dstImag: destination images. Can be NULL.
 * \param direction: 1 = forward, -1 = inverse.
 * \param bForceFFT: if true, the images are resampled to make the dimensions a power of 2.
 * \param bMagnitude: if true, the real part returns the magnitude, the imaginary part returns the phase
 * \return true if everything is ok
 */
/*
bool CxImage::FFT2(CxImage* srcReal, CxImage* srcImag, CxImage* dstReal, CxImage* dstImag,
				   long direction, bool bForceFFT, bool bMagnitude)
{
	//check if there is something to convert
	if (srcReal==NULL && srcImag==NULL) return false;

	long w,h;
	//get width and height
	if (srcReal) {
		w=srcReal->GetWidth();
		h=srcReal->GetHeight();
	} else {
		w=srcImag->GetWidth();
		h=srcImag->GetHeight();
	}

	bool bXpow2 = IsPowerof2(w);
	bool bYpow2 = IsPowerof2(h);
	//if bForceFFT, width AND height must be powers of 2
	if (bForceFFT && !(bXpow2 && bYpow2)) {
		long i;
		
		i=0;
		while((1<<i)<w) i++;
		w=1<<i;
		bXpow2=true;

		i=0;
		while((1<<i)<h) i++;
		h=1<<i;
		bYpow2=true;
	}

	// I/O images for FFT
	CxImage *tmpReal,*tmpImag;

	// select output
	tmpReal = (dstReal) ? dstReal : srcReal;
	tmpImag = (dstImag) ? dstImag : srcImag;

	// src!=dst -> copy the image
	if (srcReal && dstReal) tmpReal->Copy(*srcReal,true,false,false);
	if (srcImag && dstImag) tmpImag->Copy(*srcImag,true,false,false);

	// dst&&src are empty -> create new one, else turn to GrayScale
	if (srcReal==0 && dstReal==0){
		tmpReal = new CxImage(w,h,8);
		tmpReal->Clear(0);
		tmpReal->SetGrayPalette();
	} else {
		if (!tmpReal->IsGrayScale()) tmpReal->GrayScale();
	}
	if (srcImag==0 && dstImag==0){
		tmpImag = new CxImage(w,h,8);
		tmpImag->Clear(0);
		tmpImag->SetGrayPalette();
	} else {
		if (!tmpImag->IsGrayScale()) tmpImag->GrayScale();
	}

	if (!(tmpReal->IsValid() && tmpImag->IsValid())){
		if (srcReal==0 && dstReal==0) delete tmpReal;
		if (srcImag==0 && dstImag==0) delete tmpImag;
		return false;
	}

	//resample for FFT, if necessary 
	tmpReal->Resample(w,h,0);
	tmpImag->Resample(w,h,0);

	//ok, here we have 2 (w x h), grayscale images ready for a FFT

	double* real;
	double* imag;
	long j,k,m;

	_complex **grid;
	//double mean = tmpReal->Mean();
	// Allocate memory for the grid 
	grid = (_complex **)malloc(w * sizeof(_complex));
	for (k=0;k<w;k++) {
		grid[k] = (_complex *)malloc(h * sizeof(_complex));
	}
	for (j=0;j<h;j++) {
		for (k=0;k<w;k++) {
			grid[k][j].x = tmpReal->GetPixelIndex(k,j)-128;
			grid[k][j].y = tmpImag->GetPixelIndex(k,j)-128;
		}
	}

	//DFT buffers
	double *real2,*imag2;
	real2 = (double*)malloc(max(w,h) * sizeof(double));
	imag2 = (double*)malloc(max(w,h) * sizeof(double));

	// Transform the rows
	real = (double *)malloc(w * sizeof(double));
	imag = (double *)malloc(w * sizeof(double));

	m=0;
	while((1<<m)<w) m++;

	for (j=0;j<h;j++) {
		for (k=0;k<w;k++) {
			real[k] = grid[k][j].x;
			imag[k] = grid[k][j].y;
		}

		if (bXpow2) FFT(direction,m,real,imag);
		else		DFT(direction,w,real,imag,real2,imag2);

		for (k=0;k<w;k++) {
			grid[k][j].x = real[k];
			grid[k][j].y = imag[k];
		}
	}
	free(real);
	free(imag);

	// Transform the columns 
	real = (double *)malloc(h * sizeof(double));
	imag = (double *)malloc(h * sizeof(double));

	m=0;
	while((1<<m)<h) m++;

	for (k=0;k<w;k++) {
		for (j=0;j<h;j++) {
			real[j] = grid[k][j].x;
			imag[j] = grid[k][j].y;
		}

		if (bYpow2) FFT(direction,m,real,imag);
		else		DFT(direction,h,real,imag,real2,imag2);

		for (j=0;j<h;j++) {
			grid[k][j].x = real[j];
			grid[k][j].y = imag[j];
		}
	}
	free(real);
	free(imag);

	free(real2);
	free(imag2);

	// converting from double to byte, there is a HUGE loss in the dynamics
	//  "nn" tries to keep an acceptable SNR, but 8bit=48dB: don't ask more 
	double nn=pow((double)2,(double)log((double)max(w,h))/(double)log((double)2)-4);
	//reversed gain for reversed transform
	if (direction==-1) nn=1/nn;
	//bMagnitude : just to see it on the screen
	if (bMagnitude) nn*=4;

	for (j=0;j<h;j++) {
		for (k=0;k<w;k++) {
			if (bMagnitude){
				tmpReal->SetPixelIndex(k,j,(BYTE)max(0,min(255,(nn*(3+log(_cabs(grid[k][j])))))));
				if (grid[k][j].x==0){
					tmpImag->SetPixelIndex(k,j,(BYTE)max(0,min(255,(128+(atan(grid[k][j].y/0.0000000001)*nn)))));
				} else {
					tmpImag->SetPixelIndex(k,j,(BYTE)max(0,min(255,(128+(atan(grid[k][j].y/grid[k][j].x)*nn)))));
				}
			} else {
				tmpReal->SetPixelIndex(k,j,(BYTE)max(0,min(255,(128 + grid[k][j].x*nn))));
				tmpImag->SetPixelIndex(k,j,(BYTE)max(0,min(255,(128 + grid[k][j].y*nn))));
			}
		}
	}

	for (k=0;k<w;k++) free (grid[k]);
	free (grid);

	if (srcReal==0 && dstReal==0) delete tmpReal;
	if (srcImag==0 && dstImag==0) delete tmpImag;

	return true;
}
*/

    _complex grid[256][256];
    double real2[256];
    double imag2[256];
    double real[256];
    double imag[256];
    BYTE ffts[256*256*2];

bool FFT2Sigs(Magick::Image *image, double * pSignatures, int scale_factor, long direction, bool bForceFFT, bool bMagnitude )
{
	if (!image) return false;
    long w,h;
	w=image->columns();
	h=image->rows();

	int iPixelSize = 1; // 1=GrayScale, 3=24bpp, 4=32bpp
    int factor = scale_factor;
    int factor_minus = factor-1;
    int mid_factor = factor/2;
	
    int iBlockSize = 16;

    Magick::Geometry geometry( scale_factor, scale_factor );
    geometry.aspect(true);
	//resample for FFT, if necessary 
	//image->sample(geometry);
	image->scale(geometry); //simple algo
	w=image->columns();
	h=image->rows();
	h=scale_factor;
	w=scale_factor;

    memset((BYTE*)grid,0,256*256*sizeof(_complex));

  register const MagickLib::PixelPacket *p;
  for (int y=0; y < (long) h; y++)
  {
    p=image->getConstPixels(0,y,w,1);
    if (p == (const MagickLib::PixelPacket *) NULL)
      break;
    for (int x=0; x < (long) w; x++)
    {
      BYTE luma = (int)(MagickLib::ScaleQuantumToChar(p->red) * 0.3 + MagickLib::ScaleQuantumToChar(p->green) * 0.59 + MagickLib::ScaleQuantumToChar(p->blue) * 0.11);
	  grid[x][y].x = luma;
	  //grid[x][y].y = luma;
      p++;
    }
  }

	//ok, here we have a (w x h), grayscale image ready for a FFT

	long m;
	int j,k;

	m=0;
	while((1<<m)<w) m++;

	for (j=0;j<h;j++) {
		for (k=0;k<w;k++) {
			real[k] = grid[k][j].x;
			imag[k] = grid[k][j].x;
		}

		//if (bXpow2) 
            FFT(direction,m,real,imag);
		//else		
        //    DFT(direction,w,real,imag,real2,imag2);

		for (k=0;k<w;k++) {
			grid[k][j].x = real[k];
			grid[k][j].y = imag[k];
		}
	}

	m=0;
	while((1<<m)<h) m++;

	for (k=0;k<w;k++) {
		for (j=0;j<h;j++) {
			real[j] = grid[k][j].x;
			imag[j] = grid[k][j].y;
		}

		//if (bYpow2) 
            FFT(direction,m,real,imag);
		//else		
        //    DFT(direction,h,real,imag,real2,imag2);

		for (j=0;j<h;j++) {
			grid[k][j].x = real[j];
			grid[k][j].y = imag[j];
		}
	}

	// converting from double to byte, there is a HUGE loss in the dynamics
	//  "nn" tries to keep an acceptable SNR, but 8bit=48dB: don't ask more 
	double nn=pow((double)2,(double)log((double)max(w,h))/(double)log((double)2)-4);
	//reversed gain for reversed transform
	if (direction==-1) nn=1/nn;
	//bMagnitude : just to see it on the screen
	if (bMagnitude) nn*=4;

    UINT len = w * h;
    BYTE * rfft = (BYTE*)ffts;
    BYTE * ifft = (BYTE*)ffts+len;
    memset(ffts,0,len*2);

	for (j=0;j<h;j++) {
		for (k=0;k<w;k++) {
			if (bMagnitude){
                *rfft++ = (BYTE)max(0,min(factor_minus,(nn*(3+log(_cabs(grid[k][j]))))));
				if (grid[k][j].x==0){
                    *ifft++ = (BYTE)max(0,min(factor_minus,(mid_factor+(atan(grid[k][j].y/0.0000000001)*nn))));
				} else {
                    *ifft++ = (BYTE)max(0,min(factor_minus,(mid_factor+(atan(grid[k][j].y/grid[k][j].x)*nn))));
				}
			} else {
                *rfft++ = (BYTE)max(0,min(factor_minus,(mid_factor + grid[k][j].x*nn)));
                *ifft++ = (BYTE)max(0,min(factor_minus,(mid_factor + grid[k][j].y*nn)));
			}
		}
	}

	int iWidth = factor;
	int iHeight = factor;
	int iRow;
	int iCol;
	int iLoopRow;
	int iLoopCol;
	double dTemp;
	int iBlockNum = 0;
	long lVal1;
	BYTE *pImage = (BYTE*)ffts;

	for(iRow = 0; iRow < iHeight; iRow += iBlockSize)
	{		
		for(iCol = 0; iCol < iWidth; iCol += iBlockSize )
		{
			// Block Processing
			pSignatures[iBlockNum] = 0.0;
			dTemp = 0.0;
			for(iLoopRow = 0; iLoopRow < iBlockSize; iLoopRow++ )
			{
				for(iLoopCol = 0; iLoopCol < iBlockSize; iLoopCol++ )
				{
					//lVal1 = pImage[((iRow+iLoopRow)*iPixelSize*iWidth) + ((iCol+iLoopCol)*iPixelSize)];
					lVal1 = pImage[ ((iRow+iLoopRow)*iWidth) + (iCol+iLoopCol) ];
					dTemp += (double) lVal1;
				}
			}
			
			pSignatures[iBlockNum++] = sqrt(dTemp);
		}
	}
	
	//
	// Normalize
	//
	for(int i = 0; i < factor; i++ )
	{
		dTemp += pSignatures[i];
	}
	
	for(int i = 0; i < factor; i++ )
	{
		pSignatures[i] /= dTemp;
	}

	return true;
}

/*
bool FFT2Sigs(Magick::Image *image, double * pSignatures, int scale_factor, long direction, bool bForceFFT, bool bMagnitude )
{
	if (!image) return false;
    long w,h;
	w=image->columns();
	h=image->rows();

	int iPixelSize = 1; // 1=GrayScale, 3=24bpp, 4=32bpp
    int factor = scale_factor;
    int factor_minus = factor-1;
    int mid_factor = factor/2;
	
    int iBlockSize = 16;

    //char buf[50];
    //sprintf(buf,"!%dx%d", scale_factor,scale_factor);
    //Magick::Geometry geometry( buf );
    Magick::Geometry geometry( scale_factor, scale_factor );
    geometry.aspect(true);
	//resample for FFT, if necessary 
	//image->sample(geometry);
	image->scale(geometry); //simple algo
	w=image->columns();
	h=image->rows();
	h=scale_factor;
	w=scale_factor;

    memset((BYTE*)grid,0,256*256*sizeof(_complex));

  register const MagickLib::PixelPacket *p;
  for (int y=0; y < (long) h; y++)
  {
    p=image->getConstPixels(0,y,w,1);
    if (p == (const MagickLib::PixelPacket *) NULL)
      break;
    for (int x=0; x < (long) w; x++)
    {
      BYTE luma = (int)(MagickLib::ScaleQuantumToChar(p->red) * 0.3 + MagickLib::ScaleQuantumToChar(p->green) * 0.59 + MagickLib::ScaleQuantumToChar(p->blue) * 0.11);
	  grid[x][y].x = luma;
	  //grid[x][y].y = luma;
      p++;
    }
  }

	//ok, here we have a (w x h), grayscale image ready for a FFT

	long m;
	int j,k;

	m=0;
	while((1<<m)<w) m++;

	for (j=0;j<h;j++) {
		for (k=0;k<w;k++) {
			real[k] = grid[k][j].x;
			imag[k] = grid[k][j].x;
		}

		//if (bXpow2) 
            FFT(direction,m,real,imag);
		//else		
        //    DFT(direction,w,real,imag,real2,imag2);

		for (k=0;k<w;k++) {
			grid[k][j].x = real[k];
			grid[k][j].y = imag[k];
		}
	}

	m=0;
	while((1<<m)<h) m++;

	for (k=0;k<w;k++) {
		for (j=0;j<h;j++) {
			real[j] = grid[k][j].x;
			imag[j] = grid[k][j].y;
		}

		//if (bYpow2) 
            FFT(direction,m,real,imag);
		//else		
        //    DFT(direction,h,real,imag,real2,imag2);

		for (j=0;j<h;j++) {
			grid[k][j].x = real[j];
			grid[k][j].y = imag[j];
		}
	}

	// converting from double to byte, there is a HUGE loss in the dynamics
	//  "nn" tries to keep an acceptable SNR, but 8bit=48dB: don't ask more 
	double nn=pow((double)2,(double)log((double)max(w,h))/(double)log((double)2)-4);
	//reversed gain for reversed transform
	if (direction==-1) nn=1/nn;
	//bMagnitude : just to see it on the screen
	if (bMagnitude) nn*=4;

    UINT len = w * h;
    BYTE * rfft = (BYTE*)ffts;
    BYTE * ifft = (BYTE*)ffts+len;
    memset(ffts,0,len*2);

	for (j=0;j<h;j++) {
		for (k=0;k<w;k++) {
			if (bMagnitude){
                *rfft++ = (BYTE)max(0,min(factor_minus,(nn*(3+log(_cabs(grid[k][j]))))));
				if (grid[k][j].x==0){
                    *ifft++ = (BYTE)max(0,min(factor_minus,(mid_factor+(atan(grid[k][j].y/0.0000000001)*nn))));
				} else {
                    *ifft++ = (BYTE)max(0,min(factor_minus,(mid_factor+(atan(grid[k][j].y/grid[k][j].x)*nn))));
				}
			} else {
                *rfft++ = (BYTE)max(0,min(factor_minus,(mid_factor + grid[k][j].x*nn)));
                *ifft++ = (BYTE)max(0,min(factor_minus,(mid_factor + grid[k][j].y*nn)));
			}
		}
	}

	int iWidth = factor;
	int iHeight = factor;
	int iRow;
	int iCol;
	int iLoopRow;
	int iLoopCol;
	double dTemp;
	int iBlockNum = 0;
	long lVal1;
	BYTE *pImage = (BYTE*)ffts;

	for(iRow = 0; iRow < iHeight; iRow += iBlockSize)
	{		
		for(iCol = 0; iCol < iWidth; iCol += iBlockSize )
		{
			// Block Processing
			pSignatures[iBlockNum] = 0.0;
			dTemp = 0.0;
			for(iLoopRow = 0; iLoopRow < iBlockSize; iLoopRow++ )
			{
				for(iLoopCol = 0; iLoopCol < iBlockSize; iLoopCol++ )
				{
					lVal1 = pImage[((iRow+iLoopRow)*iPixelSize*iWidth) + ((iCol+iLoopCol)*iPixelSize)];
					dTemp += (double) lVal1;
				}
			}
			
			pSignatures[iBlockNum] = sqrt(dTemp);
			iBlockNum++;
		}
	}
	
	//
	// Normalize
	//
	for(int i = 0; i < factor; i++ )
	{
		dTemp += pSignatures[i];
	}
	
	for(int i = 0; i < factor; i++ )
	{
		pSignatures[i] /= dTemp;
	}

	return true;
}
*/



/*
bool FFT2Sigs2(Magick::Image *image, double * pSignatures, int scale_factor, long direction, bool bForceFFT, bool bMagnitude )
{
	if (!image) return false;
    long w,h;
	w=image->columns();
	h=image->rows();

	int iPixelSize = 1; // 1=GrayScale, 3=24bpp, 4=32bpp
    int factor = scale_factor;
    int factor_minus = factor-1;
    int mid_factor = factor/2;
	
    int iBlockSize = 16;

    Magick::Geometry geometry( scale_factor, scale_factor );
    geometry.aspect(true);
	//resample for FFT, if necessary 
	//image->sample(geometry);
	image->scale(geometry); //simple algo
	h=scale_factor;
	w=scale_factor;

  BYTE * pLumas = new BYTE[w*h];
  BYTE * pixelssrc = (BYTE*)pLumas;

  memset(pixelssrc,0,w*h);

  register const MagickLib::PixelPacket *p;
  BYTE minLuma = 0xFF, maxLuma = 0, midLuma;
  for (int y=0; y < (long) h; y++)
  {
    p=image->getConstPixels(0,y,w,1);
    if (p == (const MagickLib::PixelPacket *) NULL)
      break;
    for (int x=0; x < (long) w; x++)
    {
      BYTE luma = (int)(MagickLib::ScaleQuantumToChar(p->red) * 0.3 + MagickLib::ScaleQuantumToChar(p->green) * 0.59 + MagickLib::ScaleQuantumToChar(p->blue) * 0.11);
      p++;
      *pixelssrc++ = luma;
      if( luma > maxLuma ){
        maxLuma = luma;
      } else if( luma < minLuma ){
        minLuma = luma;
      }
    }
  }
  midLuma = (maxLuma - minLuma) / 2;
  BYTE pctLuma = midLuma / 10; //bias 2
  (midLuma < 127) ? midLuma += pctLuma : midLuma -= pctLuma;
  midLuma = minLuma + midLuma;

    memset((BYTE*)grid,0,256*256*sizeof(_complex));

  pixelssrc = (BYTE*)pLumas;
  for (int y=0; y < (long) h; y++)
  {
    for (int x=0; x < (long) w; x++)
    {
      BYTE luma = *pixelssrc;
      if( luma > midLuma ){
		grid[x][y].x = 1;
		grid[x][y].y = 1;
      }else{
		grid[x][y].x = 0;
		grid[x][y].y = 0;
      }
      pixelssrc++;
    }
  }

	//ok, here we have a (w x h), grayscale image ready for a FFT

	long m;
	int j,k;

	m=0;
	while((1<<m)<w) m++;

	for (j=0;j<h;j++) {
        for (k=0;k<w;k++){
			real[k] = grid[k][j].x;
			imag[k] = grid[k][j].y;
        }
        FFT(direction,m,real,imag);
        for (k=0;k<w;k++) {
			grid[k][j].x = real[k];
			grid[k][j].y = imag[k];
        }
	}

	m=0;
	while((1<<m)<h) m++;

	for (k=0;k<w;k++) {
        for (j=0;j<h;j++) {
			real[j] = grid[k][j].x;
			imag[j] = grid[k][j].y;
        }
        FFT(direction,m,real,imag);
        for (j=0;j<h;j++) {
			grid[k][j].x = real[j];
			grid[k][j].y = imag[j];
        }
	}

	// converting from double to byte, there is a HUGE loss in the dynamics
	//  "nn" tries to keep an acceptable SNR, but 8bit=48dB: don't ask more 
	double nn=pow((double)2,(double)log((double)max(w,h))/(double)log((double)2)-4);
	//reversed gain for reversed transform
	if (direction==-1) nn=1/nn;
	//bMagnitude : just to see it on the screen
	if (bMagnitude) nn*=4;

    UINT len = w * h;
    BYTE * rfft = (BYTE*)ffts;
    BYTE * ifft = (BYTE*)ffts+len;
    memset(ffts,0,len*2);

	for (j=0;j<h;j++) {
		for (k=0;k<w;k++) {
			if (bMagnitude){
                *rfft++ = (BYTE)max(0,min(factor_minus,(nn*(3+log(_cabs(grid[k][j]))))));
				if (grid[k][j].x==0){
                    *ifft++ = (BYTE)max(0,min(factor_minus,(mid_factor+(atan(grid[k][j].y/0.0000000001)*nn))));
				} else {
                    *ifft++ = (BYTE)max(0,min(factor_minus,(mid_factor+(atan(grid[k][j].y/grid[k][j].x)*nn))));
				}
			} else {
                *rfft++ = (BYTE)max(0,min(factor_minus,(mid_factor + grid[k][j].x*nn)));
                *ifft++ = (BYTE)max(0,min(factor_minus,(mid_factor + grid[k][j].y*nn)));
			}
		}
	}

	int iWidth = factor;
	int iHeight = factor;
	int iRow;
	int iCol;
	int iLoopRow;
	int iLoopCol;
	double dTemp;
	int iBlockNum = 0;
	long lVal1;
	BYTE *pImage = (BYTE*)ffts;

	for(iRow = 0; iRow < iHeight; iRow += iBlockSize)
	{		
		for(iCol = 0; iCol < iWidth; iCol += iBlockSize )
		{
			// Block Processing
			pSignatures[iBlockNum] = 0.0;
			dTemp = 0.0;
			for(iLoopRow = 0; iLoopRow < iBlockSize; iLoopRow++ )
			{
				for(iLoopCol = 0; iLoopCol < iBlockSize; iLoopCol++ )
				{
					lVal1 = pImage[((iRow+iLoopRow)*iPixelSize*iWidth) + ((iCol+iLoopCol)*iPixelSize)];
					dTemp += (double) lVal1;
				}
			}
			
			pSignatures[iBlockNum] = sqrt(dTemp);
			iBlockNum++;
		}
	}
	
	//
	// Normalize
	//
	for(int i = 0; i < factor; i++ )
	{
		dTemp += pSignatures[i];
	}
	
	for(int i = 0; i < factor; i++ )
	{
		pSignatures[i] /= dTemp;
	}

	return true;
}

bool FFT2Sigs3(Magick::Image *image, double * pSignatures, int scale_factor, long direction, bool bForceFFT, bool bMagnitude )
{
	if (!image) return false;
    long w,h;
	w=image->columns();
	h=image->rows();

	int iPixelSize = 1; // 1=GrayScale, 3=24bpp, 4=32bpp
    int factor = scale_factor;
    int factor_minus = factor-1;
    int mid_factor = factor/2;
	
    int iBlockSize = 16;

    Magick::Geometry geometry( scale_factor, scale_factor );
    geometry.aspect(true);
	//resample for FFT, if necessary 
	//image->sample(geometry);
	image->scale(geometry); //simple algo
	h=scale_factor;
	w=scale_factor;

  BYTE * pLumas = new BYTE[w*h];
  BYTE * pixelssrc = (BYTE*)pLumas;

  memset(pixelssrc,0,w*h);

  register const MagickLib::PixelPacket *p;
  BYTE minLuma = 0xFF, maxLuma = 0, midLuma;
  for (int y=0; y < (long) h; y++)
  {
    p=image->getConstPixels(0,y,w,1);
    if (p == (const MagickLib::PixelPacket *) NULL)
      break;
    for (int x=0; x < (long) w; x++)
    {
      BYTE luma = (int)(MagickLib::ScaleQuantumToChar(p->red) * 0.3 + MagickLib::ScaleQuantumToChar(p->green) * 0.59 + MagickLib::ScaleQuantumToChar(p->blue) * 0.11);
      p++;
      *pixelssrc++ = luma;
      if( luma > maxLuma ){
        maxLuma = luma;
      } else if( luma < minLuma ){
        minLuma = luma;
      }
    }
  }
  midLuma = (maxLuma - minLuma) / 2;
  BYTE pctLuma = midLuma / 10; //bias 2
  (midLuma < 127) ? midLuma += pctLuma : midLuma -= pctLuma;
  midLuma = minLuma + midLuma;

    memset((BYTE*)grid,0,256*256*sizeof(_complex));

  pixelssrc = (BYTE*)pLumas;
  for (int y=0; y < (long) h; y++)
  {
    for (int x=0; x < (long) w; x++)
    {
      BYTE luma = *pixelssrc;
      if( luma > midLuma ){
		grid[x][y].x = 1;
      }else{
		grid[x][y].x = 0;
      }
      pixelssrc++;
    }
  }

	//ok, here we have a (w x h), grayscale image ready for a FFT

	long m;
	int j,k;

	m=0;
	while((1<<m)<w) m++;

	for (j=0;j<h;j++) {
		for (k=0;k<w;k++)
			real[k] = grid[k][j].x;
        FFT(direction,m,real,imag);
		for (k=0;k<w;k++)
			grid[k][j].x = real[k];
	}

	m=0;
	while((1<<m)<h) m++;

	for (k=0;k<w;k++) {
		for (j=0;j<h;j++)
			real[j] = grid[k][j].x;
        FFT(direction,m,real,imag);
		for (j=0;j<h;j++)
			grid[k][j].x = real[j];
	}

	// converting from double to byte, there is a HUGE loss in the dynamics
	//  "nn" tries to keep an acceptable SNR, but 8bit=48dB: don't ask more 
	double nn=pow((double)2,(double)log((double)max(w,h))/(double)log((double)2)-4);
	//reversed gain for reversed transform
	if (direction==-1) nn=1/nn;
	//bMagnitude : just to see it on the screen
	if (bMagnitude) nn*=4;

    UINT len = w * h;
    BYTE * rfft = (BYTE*)ffts;
    memset(ffts,0,len*2);

	for (j=0;j<h;j++) {
		for (k=0;k<w;k++) {
			if (bMagnitude){
                *rfft++ = (BYTE)max(0,min(factor_minus,(nn*(3+log(_cabs(grid[k][j]))))));
			} else {
                *rfft++ = (BYTE)max(0,min(factor_minus,(mid_factor + grid[k][j].x*nn)));
			}
		}
	}

	int iWidth = factor;
	int iHeight = factor;
	int iRow;
	int iCol;
	int iLoopRow;
	int iLoopCol;
	double dTemp;
	int iBlockNum = 0;
	long lVal1;
	BYTE *pImage = (BYTE*)ffts;

	for(iRow = 0; iRow < iHeight; iRow += iBlockSize)
	{		
		for(iCol = 0; iCol < iWidth; iCol += iBlockSize )
		{
			// Block Processing
			pSignatures[iBlockNum] = 0.0;
			dTemp = 0.0;
			for(iLoopRow = 0; iLoopRow < iBlockSize; iLoopRow++ )
			{
				for(iLoopCol = 0; iLoopCol < iBlockSize; iLoopCol++ )
				{
					lVal1 = pImage[((iRow+iLoopRow)*iPixelSize*iWidth) + ((iCol+iLoopCol)*iPixelSize)];
					dTemp += (double) lVal1;
				}
			}
			
			pSignatures[iBlockNum] = sqrt(dTemp);
			iBlockNum++;
		}
	}
	
	//
	// Normalize
	//
	for(int i = 0; i < factor; i++ )
	{
		dTemp += pSignatures[i];
	}
	
	for(int i = 0; i < factor; i++ )
	{
		pSignatures[i] /= dTemp;
	}

	return true;
}

double dffts[256*256*2];
bool FFT2Sigs4(Magick::Image *image, double * pSignatures, int scale_factor, long direction, bool bForceFFT, bool bMagnitude )
{
	if (!image) return false;
    long w,h;
	w=image->columns();
	h=image->rows();

    Magick::Geometry geometry( scale_factor, scale_factor );
	//resample for FFT, if necessary 
	//image->sample(geometry);
    geometry.aspect(true);
	image->scale(geometry); //simple algo
	h=scale_factor;
	w=scale_factor;

	int iPixelSize = 1; // 1=GrayScale, 3=24bpp, 4=32bpp
    int factor = scale_factor;
    int factor_minus = factor-1;
    int mid_factor = factor/2;
	
    int iBlockSize = 16;

  BYTE * pLumas = new BYTE[w*h];
  BYTE * pixelssrc = (BYTE*)pLumas;

  memset(pixelssrc,0,w*h);

  register const MagickLib::PixelPacket *p;
  BYTE minLuma = 0xFF, maxLuma = 0, midLuma;
  for (int y=0; y < (long) h; y++)
  {
    p=image->getConstPixels(0,y,w,1);
    if (p == (const MagickLib::PixelPacket *) NULL)
      break;
    for (int x=0; x < (long) w; x++)
    {
      BYTE luma = (int)(MagickLib::ScaleQuantumToChar(p->red) * 0.3 + MagickLib::ScaleQuantumToChar(p->green) * 0.59 + MagickLib::ScaleQuantumToChar(p->blue) * 0.11);
      p++;
      *pixelssrc++ = luma;
      if( luma > maxLuma ){
        maxLuma = luma;
      } else if( luma < minLuma ){
        minLuma = luma;
      }
    }
  }
  midLuma = (maxLuma - minLuma) / 2;
  BYTE pctLuma = midLuma / 10; //bias 2
  (midLuma < 127) ? midLuma += pctLuma : midLuma -= pctLuma;
  midLuma = minLuma + midLuma;

    memset((BYTE*)grid,0,256*256*sizeof(_complex));
  pixelssrc = (BYTE*)pLumas;
  for (int y=0; y < (long) h; y++)
  {
    for (int x=0; x < (long) w; x++)
    {
      BYTE luma = *pixelssrc;
      if( luma > midLuma ){
		grid[x][y].x = 1;
      }else{
		grid[x][y].x = 0;
      }
      pixelssrc++;
    }
  }

	//ok, here we have a (w x h), grayscale image ready for a FFT

	long j,k,m;

	m=0;
	while((1<<m)<w) m++;

	for (j=0;j<h;j++) {
		for (k=0;k<w;k++) {
			real[k] = grid[k][j].x;
		}

        FFT(direction,m,real,imag);

		for (k=0;k<w;k++) {
			grid[k][j].x = real[k];
		}
	}

	m=0;
	while((1<<m)<h) m++;

	for (k=0;k<w;k++) {
		for (j=0;j<h;j++) {
			real[j] = grid[k][j].x;
		}

        FFT(direction,m,real,imag);

		for (j=0;j<h;j++) {
			grid[k][j].x = real[j];
		}
	}

	int iWidth = factor;
	int iHeight = factor;
	int iRow;
	int iCol;
	int iLoopRow;
	int iLoopCol;
	double dTemp;
	int iBlockNum = 0;

    double * drfft = (double*)dffts;
	for (j=0;j<h;j++)
		for (k=0;k<w;k++)
            *drfft++ = grid[k][j].x;

    drfft = (double*)dffts;
	for(iRow = 0; iRow < iHeight; iRow += iBlockSize)
	{		
		for(iCol = 0; iCol < iWidth; iCol += iBlockSize )
		{
			pSignatures[iBlockNum] = 0.0;
			dTemp = 0.0;
			for(iLoopRow = 0; iLoopRow < iBlockSize; iLoopRow++ )
			{
				for(iLoopCol = 0; iLoopCol < iBlockSize; iLoopCol++ )
				{
					dTemp += drfft[((iRow+iLoopRow)*iPixelSize*iWidth) + ((iCol+iLoopCol)*iPixelSize)];
				}
			}
			
			pSignatures[iBlockNum] = sqrt(dTemp);
			iBlockNum++;
		}
	}

    delete [] pLumas;
	for(int i = 0; i < factor; i++ )
	{
		dTemp += pSignatures[i];
	}
	
	for(int i = 0; i < factor; i++ )
	{
		pSignatures[i] /= dTemp;
	}

	return true;
}
*/
